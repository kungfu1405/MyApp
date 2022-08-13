using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using static DbData.Protos.ContinentServices;
using Xunit.Extensions.Ordering;
using Mic.Core.DataTypes;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class ContinentShould
    {
        private readonly ContinentServicesClient client;
        private static int countryId;

        public ContinentShould(TestServerHosting server)
        {
            client = new ContinentServicesClient(server.GrpcChannel);
        }
        [Fact]
        public async Task GetList()
        {
            var response = await client.GetAllAsync(new EmptyRequest());

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListContinentResponse));

            response.Data.Should().NotBeNullOrEmpty();
        }
    }
}
