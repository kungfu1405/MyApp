using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using Xunit.Extensions.Ordering;
using Mic.Core.DataTypes;
using static DbData.Protos.UserProfileServices;

namespace DbData.Service.IntegrationTest.DbDataServices
{
    [Collection(TestCollections.ServiceIntegration)]
    public class UserProfileShould
    {
        private readonly UserProfileServicesClient client;
        public static string userProfileId;  
        public UserProfileShould(TestServerHosting server)
        {
            client = new UserProfileServicesClient(server.GrpcChannel);
        }

        [Fact, Order(1)]
        public async Task Add()
        {
            var request = new UserProfileStruct
            {
                BannerUrl = "/cover/1.png",
                Intro = "Intro NEW - 1"
            };

            var response = await client.AddAsync(request);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            userProfileId = response.Data.ToString();
        }

        [Fact, Order(2)]
        public async Task Edit()
        {
            var userP = await client.GetAsync(new IdRequest { Id = userProfileId });
            userP.Intro = "Intro edited - 1";

            var response = await client.EditAsync(userP);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
        }

        [Fact, Order(3)]
        public async Task AddFollower()
        {
            string userId = "27B542D2-A64F-4843-AA92-09D6C6928803";
            string following = "BB8ED806-84A4-4BCE-B62A-E81CDF0547D1";

            var request = new UserFollowStruct
            {
                UserId = userId,
                UserFollowingId = following
            };

            var response = await client.AddFollowAsync(request);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
        }

        [InlineData("27B542D2-A64F-4843-AA92-09D6C6928803"), Order(4)]
        public async Task GetFollowers(string Id)
        {
            var response = await client.GetFollowersAsync(new IdRequest
            {
                Id = Id
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListUserFollowResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }
              

        [Theory]
        [InlineData("BB8ED806-84A4-4BCE-B62A-E81CDF0547D1"), Order(5)]
        public async Task GetFollowing(string Id)
        {
            var response = await client.GetFollowingsAsync(new IdRequest
            {
                Id = Id
            });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListUserFollowResponse));

            response.Data.Should().NotBeNullOrEmpty();
            response.TotalRecords.Should().BeGreaterThan(0);
        }

        [Fact, Order(6)]
        public async Task RemoveFollow()
        {
            string userId = "27B542D2-A64F-4843-AA92-09D6C6928803";
            string following = "BB8ED806-84A4-4BCE-B62A-E81CDF0547D1";

            var request = new UserFollowStruct
            {
                UserId = userId,
                UserFollowingId = following
            };

            var response = await client.RemoveFollowAsync(request);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
        }
    }
}
