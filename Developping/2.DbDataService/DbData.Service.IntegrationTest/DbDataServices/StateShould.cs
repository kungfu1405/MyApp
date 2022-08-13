using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using static DbData.Protos.StateServices;
using Xunit.Extensions.Ordering;
using Mic.Core.DataTypes;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class StateShould
    {
        private readonly StateServicesClient client;
        public static int stateId;

        private CountryShould countryShould;

        public StateShould(TestServerHosting server)
        {
            countryShould = new CountryShould(server);

            client = new StateServicesClient(server.GrpcChannel);
        }
        public int GetCountryId()
        {
            return countryShould.GetCountryId();
        }
        public int GetStateId()
        {
            return stateId;
        }

        [Fact, Order(1)]
        public async Task Add()
        {
            await countryShould.Add();
            countryShould.GetCountryId().Should().BeGreaterThan(0);

            var entry = new StateStruct
            {
                Name = "New State " + dStr.RandomText(5),
                CountryId = countryShould.GetCountryId(),
                Iso2 = dStr.RandomText(2)
            };
            var response = await client.AddAsync(entry);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));

            response.Data.Should().NotBeNullOrEmpty();

            stateId = dNum.ToInt(response.Data);
            stateId.Should().BeGreaterThan(0);
        }

        [Fact, Order(5)]
        public async Task Edit()
        {
            var entry = new StateStruct
            {
                Id = stateId,
                CountryId = countryShould.GetCountryId(),
                Name = "New State - Edited " + dStr.RandomText(5),
                Iso2 = dStr.RandomText(2)
            };

            var response = await client.EditAsync(entry);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(6)]
        public async Task Delete()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = stateId.ToString() });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");

            stateId = 0;
            await countryShould.Delete();
            countryShould.GetCountryId().Should().Be(0);
        }

        [Fact, Order(2)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new IdRequest { Id = stateId.ToString() });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(StateStruct));

            response.Id.Should().BeGreaterThan(0);
            response.Name.Should().NotBeNullOrEmpty();
        }

        [Fact, Order(3)]
        public async Task GetOne_NotFound()
        {
            var response = await client.GetAsync(new IdRequest { Id = "99999" });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(StateStruct));

            response.Name.Should().BeNullOrEmpty();
        }

        [Fact, Order(4)]
        public async Task GetList()
        {
            var response = await client.GetListAsync(new StateFilter
            {
                Name = "State"
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListStateResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }
    }
}
