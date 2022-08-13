using DbData.Dal;
using DbData.Entities;
using DbData.Entities.Models;
using Mic.Core.DataTypes;
using Mic.Core.Entities;
using Mic.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbData.Bll
{
    public class ExperienceBll : BllDbDataBase
    {
        public ExperienceBll(IDbDataContext context) : base(context)
        {
        }

        public async Task<EExperience> Add(EExperience entry)
        {
            if (entry.ExperienceLanguages == null || !entry.ExperienceLanguages.Any())
                throw new InvalidInputException("Invalid Input");

            if(string.IsNullOrWhiteSpace(entry.RouteUri))
                entry.RouteUri = dStr.ToASCII(entry.ExperienceLanguages.First().Title).ToLower().Replace(' ', '-');
            else
                entry.RouteUri = dStr.ToASCII(entry.RouteUri).ToLower();

            var result = await ExperienceDao.Add(entry);

            // Tag
            if (entry.Tags.Any())
            {
                result.ExperienceTags = new List<EExperienceTag>();
                foreach (var tag in entry.Tags)
                {
                    tag.Name = tag.Name.Trim();
                    var itm = await TagDao.Add(tag);

                    result.ExperienceTags.Add(new EExperienceTag
                    {
                        ExperienceId = result.Id,
                        TagId = itm.Id
                    });
                }
            }
            await ExperienceDao.Edit(result);
            return result;
        }

        public async Task Edit(EExperience entry)
        {
            if (new Guid().Equals(entry.Id))
                throw new InvalidInputException("Invalid data");

            if (entry.ExperienceLanguages == null || !entry.ExperienceLanguages.Any())
                throw new InvalidInputException("Invalid Input");

            var experience = await ExperienceDao.Get(entry.Id);
            if(experience == null)
                throw new DataNotFoundException("Experience not found");

            //experience.ExperienceTags.Clear();
            //experience.ExperienceLanguages.Clear();

            // Tag
            if (entry.Tags.Any())
            {
                //entry.ExperienceTags = new List<EExperienceTag>();
                foreach (var tag in entry.Tags)
                {
                    tag.Name = tag.Name.Trim();
                    var itm = await TagDao.Add(tag);
                    var expTag = new EExperienceTag
                    {
                        ExperienceId = entry.Id,
                        TagId = itm.Id
                    };

                    await ExperienceDao.AddExperienceTag(new EExperienceTag
                    {
                        ExperienceId = entry.Id,
                        TagId = itm.Id
                    });
                }
            }

            entry.Tags.Clear();
            entry.RouteUri = dStr.ToASCII(entry.RouteUri).ToLower();
            await ExperienceDao.Edit(entry);
        }

        public async Task Delete(Guid id)
        {
            if (new Guid().Equals(id))
                throw new InvalidInputException("Invalid data");

            var experience = await ExperienceDao.Get(id);
            if (experience == null)
                throw new DataNotFoundException("Experience not found");


            experience.ExperienceTags.Clear();
            experience.ExperienceLanguages.Clear();
            experience.ExperienceAttractionRefs.Clear();

            await ExperienceSessionImageDao.DeleteAll(id);
            await ExperienceSessionDao.DeleteBy(id);
            await ExperienceDao.Delete(id);
        }

        public async Task<EExperience> Get(Guid id, string defaultLang = "")
        {
            var result = new Guid().Equals(id) ? null : await ExperienceDao.Get(id);
            result.ExperienceLanguages = await ExperienceLanguageDao.GetList(result.Id);
            if (!string.IsNullOrEmpty(defaultLang))
            {
                result.ExperienceLanguage = result.ExperienceLanguages.SingleOrDefault(e => e.LangCode == defaultLang);
                if (result.ExperienceLanguage == null)
                    result.ExperienceLanguage = result.ExperienceLanguages.First();
            }
            return result;
        }

        public async Task<EExperience> Get(string routeUri, string langCode = "", string defaultLang = "")
        {
            var result = new Guid().Equals(routeUri) ? null : await ExperienceDao.Get(routeUri);
            if (result == null)
                return null;

            // Language
            if (!string.IsNullOrEmpty(langCode))
                result.ExperienceLanguage = await ExperienceLanguageDao.Get(result.Id, langCode, defaultLang);
            else
                result.ExperienceLanguages = await ExperienceLanguageDao.GetList(result.Id);

            // Sessions
            result.ExperienceSessions = await ExperienceSessionDao.GetList(result.Id, langCode: langCode, defaultLang: defaultLang);
            if (result.ExperienceSessions.Any())
            {
                foreach (var session in result.ExperienceSessions)
                {
                    session.Images = await ExperienceSessionImageDao.GetList(result.Id, session.Id);
                    if (session.AttractionId.HasValue && session.AttractionId != Guid.Empty)
                        session.Attraction = await AttractionDao.Get(session.AttractionId);
                }
            }

            return result;
        }

        public async Task<PagingResult<EExperience>> GetList(ExperienceFilters filter = null)
        {
            var result = await ExperienceDao.GetList(filter);
            if (result.TotalRecords > 0)
            {
                var lang = string.IsNullOrEmpty(filter.LangCode) ? filter.DefaultLang : filter.LangCode;
                foreach (var experience in result.Data)
                {
                    experience.ExperienceLanguage = await ExperienceLanguageDao.Get(experience.Id, lang, filter.DefaultLang);
                    experience.Tags = await TagDao.GetListTag(new TagFilters { ExperienceId = experience.Id });
                }
            }
            return result;
        }
    }
}
