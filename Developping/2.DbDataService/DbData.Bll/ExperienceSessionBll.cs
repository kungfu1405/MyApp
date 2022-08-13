using DbData.Dal;
using DbData.Entities;
using Mic.Core.DataTypes;
using Mic.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class ExperienceSessionBll : BllDbDataBase
    {
        public ExperienceSessionBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EExperienceSession> Add(EExperienceSession entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Title)
                || string.IsNullOrWhiteSpace(entry.LangCode))
                throw new InvalidInputException("Invalid Input");

            entry.LangCode = entry.LangCode.Trim().ToLower();
            if (new Guid().Equals(entry.ExperienceId))
            {
                // add new Experience
                //Remove Special char
                Regex reg = new Regex("[-*'\",_&#^@]");
                var expTitle = reg.Replace(entry.Title, string.Empty);

                //remove multispace to one
                Regex regexSpace = new Regex("[ ]{2,}");
                expTitle = regexSpace.Replace(expTitle, " ");

                var exp = new EExperience
                {
                    RouteUri = dStr.ToASCII(expTitle).ToLower().Replace(' ', '-'),
                    TotalComments = 0,
                    TotalLikes = 0,
                    TotalRates = 0,
                    AvgRates = 0,
                    PublishDate = DateTime.UtcNow,
                    Status = EnumPostStatus.Published
                };

                if (entry.Images.Any())
                    exp.ThumbnailUrl = entry.Images.First().ImagerUrl;
                if (entry.UserId.HasValue)
                    exp.AuthorId = entry.UserId.Value;
                if (entry.AttractionId.HasValue)
                {
                    var att = await AttractionDao.Get(entry.AttractionId);
                    if (att != null)
                    {
                        exp.DestinationId = att.DestinationId;
                    }
                }
                var experience = await ExperienceDao.Add(exp);
                entry.ExperienceId = experience.Id;

                //Update count experience
                if (entry.UserId.HasValue)
                    await UserProfileDao.UpdateCount(EnumStatisticType.Experience, entry.UserId.Value, 1);

                await updateExperienceLanguage(entry);

                // experience Attraction
                if (entry.AttractionId.HasValue)
                {
                    await ExperienceDao.AddAttraction(new EExperienceAttractionRef
                    {
                        AttractionId = entry.AttractionId.Value,
                        ExperienceId = experience.Id
                    });
                }
            }

            // add session
            entry.Status = EnumPostStatus.Published;
            var session = await ExperienceSessionDao.Add(entry.Copy());
            entry.Id = session.Id;
            entry.ExperienceId = session.ExperienceId;

            // add images to session
            await addImages(entry);

            session.Images = await ExperienceSessionImageDao.GetList(session.ExperienceId, session.Id);
            return session;
        }

        public async Task Edit(EExperienceSession entry)
        {
            if (new Guid().Equals(entry.Id)
                || new Guid().Equals(entry.ExperienceId)
                || string.IsNullOrWhiteSpace(entry.Title)
                || string.IsNullOrWhiteSpace(entry.LangCode))
                throw new InvalidInputException("Invalid data");

            var session = await ExperienceSessionDao.Get(entry.Id, langCode: entry.LangCode);
            if (session == null)
                throw new DataNotFoundException($"Section '{entry.Title}' not found");

            entry.LangCode = entry.LangCode.Trim().ToLower();
            var experience = await ExperienceDao.Get(entry.ExperienceId);

            // Update Title, Desc for Experience if it's First Session
            var sessions = await ExperienceSessionDao.GetList(entry.ExperienceId, langCode: entry.LangCode);
            if (sessions.Any() && sessions.First().Id.Equals(entry.Id))
            {
                await updateExperienceLanguage(entry);

                // Update Thumbnail to first Image
                if (entry.Images.Any()
                    && !entry.Images.First().ImagerUrl.Equals(experience.ThumbnailUrl))
                {
                    experience.ThumbnailUrl = entry.Images.First().ImagerUrl;
                }
            }

            if (session.AttractionId != entry.AttractionId)
            {
                if (sessions.Count(e => e.AttractionId == session.AttractionId) == 1)
                {
                    var oldAttRef = experience.ExperienceAttractionRefs.SingleOrDefault(e => e.AttractionId == session.AttractionId);
                    experience.ExperienceAttractionRefs.Remove(oldAttRef);
                }
                // experience Attraction
                if (entry.AttractionId.HasValue && entry.AttractionId.Value != Guid.Empty)
                {
                    await ExperienceDao.AddAttraction(new EExperienceAttractionRef
                    {
                        AttractionId = entry.AttractionId.Value,
                        ExperienceId = experience.Id
                    });

                    var att = await AttractionDao.Get(entry.AttractionId);
                    if (att != null && att.DestinationId != experience.DestinationId)
                    {
                        experience.DestinationId = att.DestinationId;
                    }
                }
            }
            //experience tags
            if (experience.ExperienceTags == null)
                experience.ExperienceTags = new List<EExperienceTag>();

            var regex = new Regex(@"#\w+");
            var matchesMain = regex.Matches(entry.Detail);
            if (matchesMain.Any())
            {                
                foreach (var strTag in matchesMain)
                {
                    var tagName = strTag.ToString().Replace("#", "").Replace(" ", "").Trim();          

                    ETag tag = new ETag { Name = tagName };
                    var itm = await TagDao.Add(tag);
                    if (experience.ExperienceTags.Where(t => t.TagId.Equals(itm.Id)).Any())
                    {
                        continue;
                    }

                    var expTag = new EExperienceTag
                    {
                        ExperienceId = experience.Id,
                        TagId = itm.Id
                    };

                    await ExperienceDao.AddExperienceTag(expTag);
                    experience.ExperienceTags.Add(expTag);
                }
            }

            await ExperienceDao.Edit(experience);
            await ExperienceSessionDao.Edit(entry);

            // renew images to session
            await ExperienceSessionImageDao.DeleteBy(entry.Id);
            await addImages(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");

            var session = await ExperienceSessionDao.GetList(sessionId: id);
            if (!session.Any())
                throw new DataNotFoundException($"Section ID '{id}' not found");

            var sessions = await ExperienceSessionDao.GetList(session.First().ExperienceId);
            if (sessions.Count() > 1 && sessions.First().Id.Equals(id))
            {
                var session2nd = sessions[1];
                await updateExperienceLanguage(session2nd);

                var experience = await ExperienceDao.Get(session2nd.ExperienceId);
                var images = await ExperienceSessionImageDao.GetList(experience.Id, session2nd.Id);
                if (images.Any())
                {
                    experience.ThumbnailUrl = images.First().ImagerUrl;
                    await ExperienceDao.Edit(experience);
                }
            }
            else
            {
                // delete Experience or not allow delete last Session ???
            }
            await ExperienceSessionImageDao.DeleteBy(id);
            await ExperienceSessionDao.Delete(id);
        }

        public async Task<EExperienceSession> Get(Guid id, string langCode, string defaultLang = "")
        {
            return (new Guid().Equals(id) || string.IsNullOrWhiteSpace(langCode)) 
                ? null : 
                await ExperienceSessionDao.Get(id, langCode, defaultLang);
        }
        public async Task<IList<EExperienceSession>> Get(Guid id)
        {
            if (new Guid().Equals(id))
                return new List<EExperienceSession>();

            var result = await ExperienceSessionDao.GetList(sessionId: id);
            
            if (result.Any())
            {
                foreach (var session in result)
                {
                    session.Images = await ExperienceSessionImageDao.GetList(result.First().ExperienceId, session.Id);

                    if (session.AttractionId.HasValue && session.AttractionId != Guid.Empty)
                        session.Attraction = await AttractionDao.Get(session.AttractionId);
                }                    
            }
            return result;
        }

        public async Task<IList<EExperienceSession>> GetList(Guid experienceId, string langCode = "", string defaultLang = "")
        {
            var result = await ExperienceSessionDao.GetList(experienceId, langCode: langCode, defaultLang: defaultLang);

            if (result.Any())
            {
                foreach (var session in result)
                {
                    session.Images = await ExperienceSessionImageDao.GetList(experienceId, session.Id);
                    if(session.AttractionId.HasValue && session.AttractionId != Guid.Empty)
                        session.Attraction = await AttractionDao.Get(session.AttractionId);
                }
            }
            return result;
        }

        private async Task addImages(EExperienceSession entry)
        {
            if (entry.Images.Any())
            {
                foreach (var img in entry.Images)
                {
                    if (string.IsNullOrWhiteSpace(img.ImagerUrl))
                        continue;
                    var sessionImage = new EExperienceSessionImage
                    {
                        ExperienceId = entry.ExperienceId,
                        ExperienceSessionId = entry.Id,
                        ImagerUrl = img.ImagerUrl,
                        CreateDate = DateTime.UtcNow,
                        Ordinal = img.Ordinal
                    };
                    await ExperienceSessionImageDao.Add(sessionImage);
                }
            }
        }

        private async Task updateExperienceLanguage(EExperienceSession entry)
        {
            var expLang = await ExperienceLanguageDao.Get(entry.ExperienceId, entry.LangCode);
            if (expLang != null)
            {
                expLang.Title = entry.Title;
                await ExperienceLanguageDao.Edit(expLang);
            }
            else
            {
                expLang = new EExperienceLanguage
                {
                    ExperienceId = entry.ExperienceId,
                    LangCode = entry.LangCode,
                    Title = entry.Title
                };
                await ExperienceLanguageDao.Add(expLang);
            }
        }
    }
}
