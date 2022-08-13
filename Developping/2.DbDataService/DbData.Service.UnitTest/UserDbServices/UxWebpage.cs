using DbData.Service.Services;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using UserDb.Protos;
using FluentAssertions;
using DbData.Service.UnitTest.Base;
using AutoMapper;
using DbData.Service.Commons;
using DbData.Protos;
using System.Linq;

namespace DbData.Service.UnitTest.UserDbServices
{
    public class UxWebpage : BaseUxTest
    {
        private readonly WebPageService service;
        private readonly InitUserData userInit;
        public UxWebpage(ITestOutputHelper output)
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            var logger = XUnitLogger.CreateLogger<WebPageService>(output);
            service = new WebPageService(userContext, mapper, logger);

            userInit = new InitUserData(userContext);
            userInit.InitWebPage();
        }

        [Fact]
        public async Task GetOne()
        {
            var response = await service.Get(new IdRequest { Id = userInit.WebPageId_Index.ToString() }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(WebPageStruct));
            response.Id.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("nolang")]
        public async Task GetOne_NotFound(string id)
        {
            var response = await service.Get(new IdRequest { Id = id }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(WebPageStruct));
            response.Id.Should().BeNullOrEmpty();
        }
        

        [Fact]
        public async Task GetAll()
        {
            var response = await service.GetList(new WebPageFilter(), null);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListWebPageResponse));
            response.TotalRecords.Should().BeGreaterThan(2);
            response.WebPages.Should().Contain(m => m.Id == userInit.WebPageId_Index.ToString());
        }
       

        [Fact]
        public async Task Edit()
        {
            var entry = await service.Get(new IdRequest { Id = userInit.WebPageId_Index.ToString() }, null);
            entry.Should().NotBeNull();
            entry.Id.Should().NotBeNullOrEmpty();

            entry.Action += " - Edited";
            var response = await service.Edit(entry, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }
    }
}
