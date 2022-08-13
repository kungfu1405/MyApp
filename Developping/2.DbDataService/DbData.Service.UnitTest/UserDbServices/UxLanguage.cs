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

namespace DbData.Service.UnitTest.UserDbServices
{
    public class UxLanguage : BaseUxTest
    {
        private readonly LanguageService service;
        private readonly InitUserData userInit;
        public UxLanguage(ITestOutputHelper output)
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            var logger = XUnitLogger.CreateLogger<LanguageService>(output);
            service = new LanguageService(userContext, mapper, logger);

            userInit = new InitUserData(userContext);
            userInit.InitLanguage();
        }

        [Fact]
        public async Task GetOne()
        {
            var response = await service.Get(new IdRequest { Id = userInit.LanguageCode }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageStruct));
            response.LangCode.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("nolang")]
        public async Task GetOne_NotFound(string id)
        {
            var response = await service.Get(new IdRequest { Id = id }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageStruct));
            response.LangCode.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetDefault()
        {
            var response = await service.GetDefault(new EmptyRequest(), null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageStruct));
            response.LangCode.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetAll()
        {
            var response = await service.GetAll(new EmptyRequest(), null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListLanguageResponse));

            response.TotalRecords.Should().BeGreaterThan(0);
            response.Languages.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllActive()
        {
            var response = await service.GetAllActive(new EmptyRequest(), null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListLanguageResponse));

            response.TotalRecords.Should().BeGreaterThan(0);
            response.Languages.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Edit()
        {
            var entry = await service.Get(new IdRequest { Id = userInit.LanguageCode }, null);
            entry.Should().NotBeNull();
            entry.LangCode.Should().NotBeNullOrEmpty();

            entry.Name += " - Edited";
            var response = await service.Edit(entry, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }
    }
}
