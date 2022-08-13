using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using static DbData.Protos.CountryServices;
using Xunit.Extensions.Ordering;
using Mic.Core.DataTypes;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class CountryShould
    {
        private readonly CountryServicesClient client;
        public static int countryId;

        public CountryShould(TestServerHosting server)
        {
            client = new CountryServicesClient(server.GrpcChannel);
        }

        public int GetCountryId()
        {
            return countryId;
        }

        [Fact, Order(1)]
        public async Task Add()
        {
            var entry = new CountryStruct
            {
                ContinentId = 1,
                Name = "New Country " + dStr.RandomText(5),
                Iso2 = dStr.RandomText(2),
                Iso3 = dStr.RandomText(3)
            };

            var response = await client.AddAsync(entry);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
            response.Data.Should().NotBeNullOrEmpty();

            countryId = dNum.ToInt(response.Data);
            countryId.Should().BeGreaterThan(0);
        }

        [Fact, Order(5)]
        public async Task Edit()
        {
            var entry = new CountryStruct
            {
                Id = countryId,
                Name = "New Country - Edited " + dStr.RandomText(5),
                Iso2 = dStr.RandomText(2),
                Iso3 = dStr.RandomText(3)
            };

            var response = await client.EditAsync(entry);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(6)]
        public async Task Delete()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = countryId.ToString() });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");

            countryId = 0;
        }

        [Fact, Order(2)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new IdRequest { Id = countryId.ToString() });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(CountryStruct));

            response.Id.Should().BeGreaterThan(0);
            response.Name.Should().NotBeNullOrEmpty();
        }

        [Fact, Order(3)]
        public async Task GetOne_NotFound()
        {
            var response = await client.GetAsync(new IdRequest { Id = "99999" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(CountryStruct));

            response.Name.Should().BeNullOrEmpty();
        }

        [Fact, Order(4)]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new CountryFilter
            {
                Name = "Country"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListCountryResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }
    }
}
