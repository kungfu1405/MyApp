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
using Xunit.Extensions.Ordering;

namespace DbData.Service.UnitTest.UserDbServices
{
    public class UxLanguageData : BaseUxTest
    {
        private readonly LanguageDataService service;
        private readonly InitUserData userInit;
        public UxLanguageData(ITestOutputHelper output)
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            var logger = XUnitLogger.CreateLogger<LanguageDataService>(output);
            service = new LanguageDataService(userContext, mapper, logger);

            userInit = new InitUserData(userContext);
            userInit.InitLanguage();
        }

        [Fact, Order(1)]
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

        [Fact, Order(2)]
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

        [Fact, Order(3)]
        public async Task Edit()
        {
            var entry = await service.Get(new LanguageDataFilter
            {
                LangCode = userInit.LanguageCode,
                LangKey = userInit.LanguageKey
            }, null);
            entry.Should().NotBeNull();
            entry.LangKey.Should().NotBeNullOrEmpty();

            entry.Value += " - Edited";
            var response = await service.Edit(entry, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(4)]
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
