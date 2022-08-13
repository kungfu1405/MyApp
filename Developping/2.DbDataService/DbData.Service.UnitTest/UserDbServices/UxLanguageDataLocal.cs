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
using Mic.Core.DataTypes;

namespace DbData.Service.UnitTest.UserDbServices
{
    public class UxLanguageDataLocal : BaseUxTest
    {
        private readonly LanguageDataLocalService service;
        private readonly InitUserData userInit;
        public UxLanguageDataLocal(ITestOutputHelper output)
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            var logger = XUnitLogger.CreateLogger<LanguageDataLocalService>(output);

            service = new LanguageDataLocalService(userContext, mapper, logger);

            userInit = new InitUserData(userContext);
            userInit.InitLanguage();
        }

        [Fact]
        public async Task GetOne()
        {
            var response = await service.Get(new LanguageDataFilter
            {
                LangCode = userInit.LanguageCode,
                LangKey = userInit.LanguageKey
            }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageDataStruct));
            response.LangKey.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetOne_NotFound()
        {
            var response = await service.Get(new LanguageDataFilter
            {
                LangCode = userInit.LanguageCode,
                LangKey = "linhtinh"
            }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageDataStruct));
            response.LangKey.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetAll()
        {
            var response = await service.GetList(new LanguageDataFilter
            {
                LangCode = userInit.LanguageCode,
            }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListLanguageDataResponse));

            response.TotalRecords.Should().BeGreaterThan(0);
            response.Data.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Add()
        {
            var langKey = dStr.RandomText(10);
            var entry = new LanguageDataStruct
            {
                LangKey = langKey,
                LangCode = userInit.LanguageCode,
                Value = "New Key"
            };
            var response = await service.Add(entry, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));

            response.StatusCode.Should().Be("200");
            response.Data.Should().Equals(langKey.ToLower());
        }

        [Fact]
        public async Task Delete()
        {
            var entry = await service.Get(new LanguageDataFilter
            {
                LangCode = userInit.LanguageCode,
                LangKey = userInit.LanguageKey
            }, null);
            entry.Should().NotBeNull();
            entry.LangKey.Should().NotBeNullOrEmpty();

            var response = await service.Delete(new IdRequest { Id = userInit.LanguageKey }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }
    }
}
