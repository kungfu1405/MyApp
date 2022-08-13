using DbData.Service.IntegrationTest.Base;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DbData.Protos;
using UserDb.Protos;
using Mic.Core.DataTypes;
using Xunit.Extensions.Ordering;
using static UserDb.Protos.WebPageServices;
using System;

namespace DbData.Service.IntegrationTest.UserDbServices
{
    [Collection(TestCollections.UserServiceIntegration)]
    public class WebpageShould
    {
        private readonly WebPageServicesClient client;
        private static string Id;

        public WebpageShould(TestServerHosting server)
        {
            client = new WebPageServicesClient(server.GrpcChannel);
        }


        [Fact, Order(1)]
        public async Task Add()
        {
            Id = Guid.NewGuid().ToString();
            var request = new WebPageStruct
            {
                Action = "Index",
                AllowAnonymous = true,
                CreateDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
                Description = "",
                DisplayName = "Index",
                Id = Id,
                ModifyDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow),
                Permissions = 1,
                WebControllerId = new Guid().ToString()
            };
            var response = await client.AddAsync(request);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
            response.Data.Should().NotBeNullOrEmpty();
        }

        [Fact, Order(2)]
        public async Task GetOne()
        {
            var response = await client.GetAsync(new IdRequest { Id = Id });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(WebPageStruct));
            response.Id.Should().Equals(Id);
        }

        [Theory, Order(3)]
        [InlineData("linhtinh")]
        public async Task GetOne_ShouldNotFound(string id)
        {
            var response = await client.GetAsync(new IdRequest { Id = id });

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(WebPageStruct));
            response.Id.Should().BeNullOrEmpty();
        }       

        [Fact, Order(5)]
        public async Task GetList()
        {
            var filer = new WebPageFilter();
            filer.Name = "In";
            var response = await client.GetListAsync(filer);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListWebPageResponse));
            response.TotalRecords.Should().Be(1);
        }

        [Fact, Order(6)]
        public async Task GetList_Empty()
        {
            var filer = new WebPageFilter();
            filer.Name = "xx";
            var response = await client.GetListAsync(filer);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListWebPageResponse));
            response.TotalRecords.Should().Be(0);
        }

        [Fact, Order(7)]
        public async Task Edit()
        {
            var entry = await client.GetAsync(new IdRequest { Id = Id }, null);
            entry.Should().NotBeNull();
            entry.Id.Should().Equals(Id.ToLower());

            entry.Action += " - Edited";
            var response = await client.EditAsync(entry);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact, Order(8)]
        public async Task Delete()
        {
            var response = await client.DeleteAsync(new IdRequest { Id = Id });
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
            Id = "";
        }
    }
}
