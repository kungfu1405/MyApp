using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using UserDb.Protos;
using FluentAssertions;
using static UserDb.Protos.UserAuthenticateServices;

namespace DbData.Service.IntegrationTest.UserDbServices
{
    [Collection(TestCollections.UserServiceIntegration)]
    public class UserAuthenticateShould
    {
        private readonly UserAuthenticateServicesClient client;
        public UserAuthenticateShould(TestServerHosting server)
        {
            client = new UserAuthenticateServicesClient(server.GrpcChannel);
        }

        [Fact]
        public async Task Login()
        {
            var request = new SigninRequest { UserName = "admin", Password = "123@123Aa" };
            var response = await client.SigninAsync(request);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(SigninResponse));

            response.Id.Should().NotBeEmpty();
            response.Message.Status.Should().Be(0);
        }

        [Fact]
        public async Task Login_Failed()
        {
            var request = new SigninRequest { UserName = "admin", Password = "linhtinh" };
            var response = await client.SigninAsync(request);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(SigninResponse));

            response.Id.Should().BeEmpty();
            response.Message.Status.Should().Be(2);
        }
    }
}
