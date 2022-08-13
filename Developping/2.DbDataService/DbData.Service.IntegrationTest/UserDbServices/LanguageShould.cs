using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using static UserDb.Protos.LanguageServices;
using Mic.Core.DataTypes;
using Xunit.Extensions.Ordering;

namespace DbData.Service.IntegrationTest.UserDbServices
{
    [Collection(TestCollections.UserServiceIntegration)]
    public class LanguageShould
    {
        private readonly LanguageServicesClient client;
        private static string langCode;

        public LanguageShould(TestServerHosting server)
        {
            client = new LanguageServicesClient(server.GrpcChannel);
        }


        [Fact, Order(1)]
        public async Task Add()
        {
            langCode = dStr.RandomText(10);
            var request = new LanguageStruct
            {
                LangCode = langCode,
                Name = "Lang_" + langCode,
                Native = langCode.Substring(2),
                IsDefault = true,
                IsRtl = false,
                Ordinal = 1,
                IsActive = true
            };
            var response = await client.AddAsync(request);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
            response.Data.Should().NotBeNullOrEmpty();
        }

        [Fact, Order(2)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new IdRequest { Id = langCode });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageStruct));
            response.LangCode.Should().Equals(langCode.ToLower());
        }

        [Theory, Order(3)]
        [InlineData("linhtinh")]
        public async Task GetOne_ShouldNotFound(string id)
        {
            var response = await client.GetAsync(new IdRequest { Id = id });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageStruct));
            response.LangCode.Should().BeNullOrEmpty();
        }

        [Fact, Order(4)]
        public async Task GetDefault()
        {
            var response = await client.GetDefaultAsync(new EmptyRequest());

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(LanguageStruct));
            response.LangCode.Should().Equals(langCode.ToLower());
        }

        [Fact, Order(5)]
        public async Task GetAll()
        {
            var response = await client.GetAllAsync(new EmptyRequest());

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListLanguageResponse));
            response.Languages.Should().NotBeEmpty();
        }

        [Fact, Order(6)]
        public async Task GetAllActive()
        {
            var response = await client.GetAllActiveAsync(new EmptyRequest());

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListLanguageResponse));

            response.TotalRecords.Should().BeGreaterThan(0);
            response.Languages.Should().NotBeEmpty();
        }

        [Fact, Order(7)]
        public async Task Edit()
        {
            var entry = await client.GetAsync(new IdRequest { Id = langCode }, null);
            entry.Should().NotBeNull();
            entry.LangCode.Should().Equals(langCode.ToLower());

            entry.Name += " - Edited";
            var response = await client.EditAsync(entry);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(8)]
        public async Task Delete()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = langCode });
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
            langCode = "";
        }
    }
}
