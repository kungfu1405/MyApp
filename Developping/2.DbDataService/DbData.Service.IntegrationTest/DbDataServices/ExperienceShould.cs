using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using static DbData.Protos.ExperienceServices;
using Xunit.Extensions.Ordering;
using DbData.Entities;
using System;
using Google.Protobuf.WellKnownTypes;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class ExperienceShould
    {
        private readonly ExperienceServicesClient client;
        private static Guid experienceId;
        private static ExperienceStruct experience;
        public ExperienceShould(TestServerHosting server)
        {
            client = new ExperienceServicesClient(server.GrpcChannel);
        }

        [Fact, Order(1)]
        public async Task Add()
        {
            var request = new ExperienceStruct
            {
                RouteUri = "trải-nghiệm-mộc-châu",
                ThumbnailUrl = "/20210125/nepal_s2_31.jpg",
                AuthorId = "27B542D2-A64F-4843-AA92-09D6C6928803",
                PublishDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc).ToTimestamp(),
                Status = (int)EnumPostStatus.Published
            };
            request.ExperienceLanguages.Add(new ExperienceLanguageStruct
            {
                LangCode = "vn",
                Title = "Bài viết thứ 1",
                Description = "Mô tả cho bài viết 1"
            });
            request.ExperienceLanguages.Add(new ExperienceLanguageStruct
            {
                LangCode = "en",
                Title = "Experience 1st",
                Description = "Desc for Experience 1"
            });
            request.Tags.Add(new TagStruct{ Name = "Experience" });
            request.Tags.Add(new TagStruct{ Name = "Kinh nghiệm" });

            var response = await client.AddAsync(request);
            response.Should().NotBeNull();

            response.Message.Should().BeOfType(typeof(ResponseMessage));
            response.Message.StatusCode.Should().Be("200");

            experienceId = new Guid(response.Message.Data);
            experience = response.Experience;
        }

        [Fact, Order(5)]
        public async Task Edit()
        {
            experience.ThumbnailUrl = "/20210125/nepal_s2_31.jpg - Edited";
            experience.ExperienceLanguages[0].Title = "Bài viết thứ 1 - Edited";
            experience.ExperienceLanguages[1].Title = "Experience 1st - Edited";
            experience.ExperienceLanguages.Add(new ExperienceLanguageStruct
            {
                ExperienceId = experienceId.ToString(),
                LangCode = "us",
                Title = "Experience 1st US",
                Description = "Desc for Experience USUSUSUSUS"
            });
            experience.Tags.Add(new TagStruct { Name = "Trải nghiệm" });

            var response = await client.EditAsync(experience);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(6)]
        public async Task Delete()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = experienceId.ToString()});
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");

            experienceId = new Guid();
            experience = null;
        }

        [Fact, Order(2)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new IdLangRequest { Id = experience.RouteUri, LangCode = "vn" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ExperienceStruct));

            response.Id.Should().NotBeNullOrEmpty();
            response.ExperienceLanguage.Should().NotBeNull();
        }

        [Fact, Order(3)]
        public async Task GetOne_NotFound()
        {
            var response = await client.GetAsync(new IdLangRequest { Id = "linhtinh", LangCode = "vn" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ExperienceStruct));

            response.Id.Should().BeNullOrEmpty();
            response.ExperienceLanguage.Should().BeNull();
        }

        [Fact, Order(4)]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new ExperienceFilter 
            { 
                LangCode = "vn"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListExperienceResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }
    }
}
