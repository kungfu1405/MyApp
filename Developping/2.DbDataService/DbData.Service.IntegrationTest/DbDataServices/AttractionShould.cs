using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using static DbData.Protos.AttractionServices;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class AttractionShould
    {
        private readonly AttractionServicesClient client;
        public AttractionShould(TestServerHosting server)
        {
            client = new AttractionServicesClient(server.GrpcChannel);
        }


        [Theory]
        [InlineData("viet-nam-an-giang-chau-doc-rung-tram-tra-su")]
        //[InlineData("27b542d2-a64f-4843-aa92-09d6c6928803")]
        public async Task GetOne(string routeUri)
        {
            var response = await client.GetAsync(new IdLangRequest { Id = routeUri, LangCode = "vn" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(AttractionStruct));

            response.Id.Should().NotBeNullOrEmpty();
            response.AttractionLanguage.Should().NotBeNull();
        }

        [Theory]
        [InlineData("00000000-84a4-4bce-b62a-e81cdf0547d1")]
        public async Task GetOne_NotFound(string id)
        {
            var response = await client.GetAsync(new IdLangRequest { Id = id, LangCode = "vn" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(AttractionStruct));

            response.Id.Should().BeNullOrEmpty();
            response.AttractionLanguage.Should().BeNull();
        }

        [Theory]
        [InlineData("D684A0F8-2B24-4B85-847A-0476458B2DA3")]
        public async Task GetBy(string id)
        {
            var response = await client.GetByAsync(new IdRequest { Id = id });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(AttractionStruct));

            response.Id.Should().NotBeNullOrEmpty();
            response.AttractionLanguage.Should().NotBeNull();
        }

        [Fact]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new AttractionFilter
            {
                LangCode = "vn"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListAttractionResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }
    }
}
