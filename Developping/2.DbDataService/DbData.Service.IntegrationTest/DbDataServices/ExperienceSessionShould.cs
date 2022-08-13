using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using static DbData.Protos.ExperienceSessionServices;
using Mic.Core.DataTypes;
using Xunit.Extensions.Ordering;
using DbData.Entities;
using System;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class ExperienceSessionShould
    {
        private readonly ExperienceSessionServicesClient client;

        private static Guid experienceId;
        private static Guid firstSessionId;
        private static Guid secondSessionId;
        public ExperienceSessionShould(TestServerHosting server)
        {
            client = new ExperienceSessionServicesClient(server.GrpcChannel);
        }

        [Fact, Order(1)]
        public async Task Add_FirstSession()
        {
            var request = new ExperienceSessionStruct
            {
                LangCode = "vn",
                Title = "Trải nghiệm Mộc Châu",
                SubTitle = "[Session 1]: #Làngnguyênthủy",
                Detail = "Điều đầu tiên mình bước xuống xe nhất quyết phải đến được ngôi làng này, cũng là điểm để lại nhiều ấn tượng nhất với mình. #Làngnguyênthủy nằm trọn dưới thung lũng nhỏ, chỉ vỏn vẹn 12 hộ dân. Không wifi, không sóng điện thoại, ở đây người ta sinh hoạt gần như tách biệt hoàn toàn với thế giới bên ngoài. Phải nói lên hình chưa mô tả được hết vẻ đẹp nơi đây các bạn à. Thơ mộng một cách lạ kì, có cảm tưởng như đang đi giữa những thung lũng Mông Cổ ấy. Mình xem review thì rất ít bài viết về ngôi làng này, một phần vì đường đi khó khiến khách du lịch ngại. Phần nữa trên google map không định vị được ngôi làng này. Nếu các bạn không tự tin thì có thể nhờ một thổ địa dẫn đường hen. Một nơi không thể bỏ qua khi đến Mộc Châu. ",
                UserId = "27B542D2-A64F-4843-AA92-09D6C6928803",
                Status = (int)EnumPostStatus.Published
            };
            request.Images.Add(new ExperienceSessionImageStruct
            {
                ImagerUrl = "/20210125/nepal_s2_1.jpg",
                Ordinal = 1
            });
            request.Images.Add(new ExperienceSessionImageStruct
            {
                ImagerUrl = "/20210125/nepal_s2.jpg",
                Ordinal = 2
            });
            var response = await client.AddAsync(request);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ExperienceSessionResponse));

            response.Message.Should().NotBeNull();
            response.Message.StatusCode.Should().Be("200");

            response.Session.Should().NotBeNull();
            response.Session.Id.Should().NotBeNullOrEmpty();
            response.Session.ExperienceId.Should().NotBeNullOrEmpty();

            experienceId = new Guid(response.Session.ExperienceId);
            firstSessionId = new Guid(response.Session.Id);
        }

        [Fact, Order(2)]
        public async Task Add_SecondSession()
        {
            var request = new ExperienceSessionStruct
            {
                ExperienceId = experienceId.ToString(),
                LangCode = "vn",
                Title = "Trải nghiệm Mộc Châu.",
                SubTitle = "[Session 2]: #ThácNàngTiên",
                Detail = "Cái tên nghe mộng mị! Mình hỏi một số người dân thì không ai biết tên này xuất phát từ đâu. Đến nơi chỉ thấy một ngọn nước đổ từ trên xuống cao đến 100m, lần đầu tiên trong đời mình thấy một ngọn thác hùng vĩ đến thế đấy. Ngọn thác 3 tầng, tầng cao nhất, đến 100m. Để lên được đây, mình phải lội quãng đường khá xa",
                UserId = "27B542D2-A64F-4843-AA92-09D6C6928803",
                Status = (int)EnumPostStatus.Published
            };
            request.Images.Add(new ExperienceSessionImageStruct
            {
                ImagerUrl = "/20210125/nepal_s2_3.jpg",
                Ordinal = 1
            });
            request.Images.Add(new ExperienceSessionImageStruct
            {
                ImagerUrl = "/20210125/nepal_s4.jpg",
                Ordinal = 2
            });
            var response = await client.AddAsync(request);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ExperienceSessionResponse));

            response.Message.Should().NotBeNull();
            response.Message.StatusCode.Should().Be("200");

            response.Session.Should().NotBeNull();
            response.Session.Id.Should().NotBeNullOrEmpty();

            secondSessionId = new Guid(response.Session.Id);
        }

        [Fact, Order(3)]
        public async Task Edit_FirstSession()
        {
            var request = new ExperienceSessionStruct
            {
                Id = firstSessionId.ToString(),
                ExperienceId = experienceId.ToString(),
                LangCode = "vn",
                Title = "Trải nghiệm Mộc Châu Edited.",
                SubTitle = "[Session 2]: #ThácNàngTiên Edited",
                Detail = "Cái tên nghe mộng mị! Mình hỏi một số người dân thì không ai biết tên này xuất phát từ đâu. Đến nơi chỉ thấy một ngọn nước đổ từ trên xuống cao đến 100m, lần đầu tiên trong đời mình thấy một ngọn thác hùng vĩ đến thế đấy. Ngọn thác 3 tầng, tầng cao nhất, đến 100m. Để lên được đây, mình phải lội quãng đường khá xa",
                UserId = "27B542D2-A64F-4843-AA92-09D6C6928803",
                Status = (int)EnumPostStatus.Published
            };
            request.Images.Add(new ExperienceSessionImageStruct
            {
                ImagerUrl = "/20210125/nepal_s2_3-Edited.jpg",
                Ordinal = 1
            });
            request.Images.Add(new ExperienceSessionImageStruct
            {
                ImagerUrl = "/20210125/nepal_s4-Edited.jpg",
                Ordinal = 2
            });

            var response = await client.EditAsync(request);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(4)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new IdLangRequest
            {
                Id = firstSessionId.ToString(),
                LangCode = "vn"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ExperienceSessionStruct));

            response.Id.Should().NotBeNullOrEmpty();
            response.Images.Should().NotBeEmpty();
        }

        [Fact, Order(5)]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new IdLangRequest
            {
                Id = experienceId.ToString(),
                LangCode = "vn"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListExperienceSessionResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }

        [Fact, Order(6)]
        public async Task Delete_Session2()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = secondSessionId.ToString() });
            response.Should().NotBeNull();
            response.Should().NotBeNull();
            response.StatusCode.Should().Be("200");

            experienceId = new Guid();
            firstSessionId = new Guid();
            secondSessionId = new Guid();
        }
    }
}
