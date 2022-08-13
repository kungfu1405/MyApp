using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using static DbData.Protos.CityServices;
using Xunit.Extensions.Ordering;
using Mic.Core.DataTypes;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class CityShould
    {
        private readonly CityServicesClient client;
        private static int cityId;
        private StateShould stateShould;

        public CityShould(TestServerHosting server)
        {
            stateShould = new StateShould(server);
            client = new CityServicesClient(server.GrpcChannel);
        }

        [Fact, Order(1)]
        public async Task Add()
        {
            await stateShould.Add();
            stateShould.GetCountryId().Should().BeGreaterThan(0);
            stateShould.GetStateId().Should().BeGreaterThan(0);

            var entry = new CityStruct
            {
                Name = "New City " + dStr.RandomText(5),
                CountryId = stateShould.GetCountryId(),
                StateId = stateShould.GetStateId(),
                Latitude = 0.1D,
                Longitude = 0.1D
            };
            var response = await client.AddAsync(entry);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));

            response.Data.Should().NotBeNullOrEmpty();

            cityId = dNum.ToInt(response.Data);
            cityId.Should().BeGreaterThan(0);
        }

        [Fact, Order(5)]
        public async Task Edit()
        {
            var entry = new CityStruct
            {
                Id = cityId,
                Name = "New City - Edited " + dStr.RandomText(5),
                CountryId = stateShould.GetCountryId(),
                StateId = stateShould.GetStateId(),
                Latitude = 0.1D,
                Longitude = 0.1D
            };

            var response = await client.EditAsync(entry);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(6)]
        public async Task Delete()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = cityId.ToString() });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");

            await stateShould.Delete();
            stateShould.GetCountryId().Should().Be(0);
            stateShould.GetStateId().Should().Be(0);
            cityId = 0;
        }

        [Fact, Order(2)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new IdRequest { Id = cityId.ToString() });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(CityStruct));

            response.Id.Should().BeGreaterThan(0);
            response.Name.Should().NotBeNullOrEmpty();
        }

        [Fact, Order(3)]
        public async Task GetOne_NotFound()
        {
            var response = await client.GetAsync(new IdRequest { Id = "99999" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(CityStruct));

            response.Name.Should().BeNullOrEmpty();
        }

        [Fact, Order(4)]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new CityFilter
            {
                Name = "City"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListCityResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }
    }
}
