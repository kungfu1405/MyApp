using AutoMapper;
using DbData.Entities;
using DbData.Protos;
using DbData.Service.Commons;
using DbData.Service.Services;
using DbData.Service.UnitTest.Base;
using FluentAssertions;
using Mic.Core.DataTypes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace DbData.Service.UnitTest.DbDataService
{
    public class UxCountry : BaseUxTest
    {
        private readonly CountryService service;
        private readonly InitDbData dbInit;
        public UxCountry(ITestOutputHelper output)
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();

            var logger = XUnitLogger.CreateLogger<CountryService>(output);
            service = new CountryService(dbContext, mapper, logger);

            dbInit = new InitDbData(dbContext);
            dbInit.InitCountry();
        }

        [Fact]
        public async Task GetOne()
        {
            var response = await service.Get(new IdRequest { Id = dbInit.CountryId.ToString() }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(CountryStruct));

            response.Id.Should().BeGreaterThan(0);
            response.Name.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("999999")]
        public async Task GetOne_NotFound(string id)
        {
            var response = await service.Get(new IdRequest { Id = id }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(CountryStruct));

            response.Name.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetList()
        {
            var response = await service.GetList(new CountryFilter 
            { 
                Name = "Country"
            }, null);
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ListCountryResponse));

            response.TotalRecords.Should().BeGreaterThan(0);
            response.Data.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Add()
        {
            var entry = new CountryStruct
            {
                Name = "New Country " + random.Next()
            };

            var response = await service.Add(entry, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
            response.Data.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Edit()
        {
            // Edit
            var editedEntry = new CountryStruct
            {
                Id = dbInit.CountryId,
                Name = "Country - Edited"
            };
            var response = await service.Edit(editedEntry, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }

        [Fact]
        public async Task Delete()
        {
            var entry = new CountryStruct
            {
                Name = "New Country " + random.Next()
            };
            var newResponse = await service.Add(entry, null);
            newResponse.Should().NotBeNull();
            newResponse.Data.Should().NotBeNullOrEmpty();


            var response = await service.Delete(new IdRequest { Id = newResponse.Data }, null);

            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(ResponseMessage));
            response.StatusCode.Should().Be("200");
        }
    }
}
