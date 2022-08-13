using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using static UserDb.Protos.UserServices;

namespace DbData.Service.IntegrationTest.UserDbServices
{
    [Collection(TestCollections.UserServiceIntegration)]
    public class UserShould
    {
        private readonly UserServicesClient client;
        public UserShould(TestServerHosting server)
        {
            client = new UserServicesClient(server.GrpcChannel);
        }

        [Theory]
        [InlineData("bb8ed806-84a4-4bce-b62a-e81cdf0547d1")]
        //[InlineData("27b542d2-a64f-4843-aa92-09d6c6928803")]
        public async Task GetOne(string id)
        {
            var response = await client.GetAsync(new IdRequest { Id = id });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(UserStruct));

            response.Id.Should().NotBeNullOrEmpty();
            response.Usergroup.Should().NotBeNull();
            response.Roles.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData("00000000-84a4-4bce-b62a-e81cdf0547d1")]
        public async Task GetOne_NotFound(string id)
        {
            var response = await client.GetAsync(new IdRequest { Id = id });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(UserStruct));

            response.Id.Should().BeNullOrEmpty();
            response.Usergroup.Should().BeNull();
            response.Roles.Should().BeEmpty();
        }
    }
}
