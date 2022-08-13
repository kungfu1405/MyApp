using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using static DbData.Protos.DestinationServices;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class DestinationShould
    {
        private readonly DestinationServicesClient client;
        public DestinationShould(TestServerHosting server)
        {
            client = new DestinationServicesClient(server.GrpcChannel);
        }


        [Theory]
        [InlineData("viet-nam-son-la-moc-chau")]
        //[InlineData("27b542d2-a64f-4843-aa92-09d6c6928803")]
        public async Task GetOne(string routeUri)
        {
            var response = await client.GetAsync(new IdLangRequest { Id = routeUri, LangCode = "vn" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(DestinationStruct));

            response.Id.Should().NotBeNullOrEmpty();
            response.DestinationLanguage.Should().NotBeNull();
        }

        [Theory]
        [InlineData("00000000-84a4-4bce-b62a-e81cdf0547d1")]
        public async Task GetOne_NotFound(string id)
        {
            var response = await client.GetAsync(new IdLangRequest { Id = id, LangCode = "vn" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(DestinationStruct));

            response.Id.Should().BeNullOrEmpty();
            response.DestinationLanguage.Should().BeNull();
        }

        [Fact]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new DestinationFilter
            {
                LangCode = "vn"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListDestinationResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }
    }
}
