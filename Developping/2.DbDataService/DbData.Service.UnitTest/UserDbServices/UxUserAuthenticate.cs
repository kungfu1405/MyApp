using DbData.Service.Services;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit;
using UserDb.Protos;
using FluentAssertions;
using DbData.Service.UnitTest.Base;

namespace DbData.Service.UnitTest.UserDbServices
{
    public class UxUserAuthenticate : BaseUxTest
    {
        private readonly UserAuthenticateService service;
        private readonly InitUserData userInit;
        public UxUserAuthenticate(ITestOutputHelper output)
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        {
            var logger = XUnitLogger.CreateLogger<UserAuthenticateService>(output);
            service = new UserAuthenticateService(userContext, logger);

            userInit = new InitUserData(userContext);
            userInit.InitUserDb();
        }

        [Fact]
        public async Task Login()
        {
            var response = await service.Signin(
                new SigninRequest { UserName = userInit.UserAdminName, Password = "123@123Aa" }
                , null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(SigninResponse));

            response.Id.Should().NotBeEmpty();
            response.Message.Status.Should().Be(0);
        }

        [Fact]
        public async Task Login_Failed()
        {
            var response = await service.Signin(
                new SigninRequest { UserName = "admin", Password = "linhtinh" }
                , null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(SigninResponse));

            response.Id.Should().BeEmpty();
            response.Message.Status.Should().Be(3);
        }
    }
}
