using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using static UserDb.Protos.LanguageServices;
using static UserDb.Protos.LanguageDataServices;
using Mic.Core.DataTypes;
using Xunit.Extensions.Ordering;

namespace DbData.Service.IntegrationTest.UserDbServices
{
    [Collection(TestCollections.UserServiceIntegration)]
    public class LanguageDataShould
    {
        private readonly LanguageDataServicesClient client;
        private readonly LanguageServicesClient languageClient;
        private static string langCode;
        private static string langKey;

        public LanguageDataShould(TestServerHosting server)
        {
            client = new LanguageDataServicesClient(server.GrpcChannel);
            languageClient = new LanguageServicesClient(server.GrpcChannel);
        }

        [Fact, Order(1)]
        public async Task Add()
        {
            langCode = dStr.RandomText(10);
            langKey = dStr.RandomText(10);

            // add new Language for testing
            var language = new LanguageStruct
            {
                LangCode = langCode,
                Name = "Lang_" + langCode,
                Native = langCode.Substring(2),
                IsDefault = true,
                IsRtl = false,
                Ordinal = 1,
                IsActive = true
            };
            var responseLanguage = await languageClient.AddAsync(language);
            responseLanguage.Should().NotBeNull();
            responseLanguage.Should().BeOfType(typeof(ResponseMessage));
            responseLanguage.StatusCode.Should().Be("200");

            var entry = new LanguageDataStruct
            {
                LangKey = langKey,
                LangCode = langCode,
                Value = "linhtinh",
                IsGroup = false
            };
            var response = await client.AddAsync(entry);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(2)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new LanguageDataFilter
            {
                LangCode = langCode,
                LangKey = langKey
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageDataStruct));
            response.LangKey.Should().Equals(langCode.ToLower());
        }

        [Fact, Order(3)]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new LanguageDataFilter
            {
                LangCode = langCode
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListLanguageDataResponse));
            response.Data.Should().NotBeEmpty();
        }

        [Fact, Order(4)]
        public async Task Edit()
        {
            var entry = await client.GetAsync(new LanguageDataFilter
            {
                LangCode = langCode,
                LangKey = langKey
            });
            entry.Should().NotBeNull();
            entry.LangCode.Should().Equals(langCode.ToLower());

            entry.Value += " - Edited";
            var response = await client.EditAsync(entry);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(5)]
        public async Task Delete()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = langKey });
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");

            var dResponse = await languageClient.DeleteAsync(new IdRequest { Id = langCode });
            dResponse.StatusCode.Should().Be("200");

            langCode = "";
            langKey = "";
        }
    }
}
