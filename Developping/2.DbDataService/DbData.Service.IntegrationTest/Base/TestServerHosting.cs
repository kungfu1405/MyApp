using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace DbData.Service.IntegrationTest.Base
{
    public class TestServerHosting : IDisposable
    {        
        public TestServer Server { get; private set; }
        public HttpClient Client { get; private set; }
        public GrpcChannel GrpcChannel { get; private set; }


        public TestServerHosting()
        {
            var webHost = new WebHostBuilder()
                .UseStartup<Startup>();

            Server = new TestServer(webHost);

            Client = Server.CreateClient();

            GrpcChannel = GrpcChannel.ForAddress(Server.BaseAddress, new GrpcChannelOptions
            {
                HttpClient = Client
            });
        }

        public void Dispose()
        {
            if (GrpcChannel != null)
            {
                GrpcChannel.Dispose();
                GrpcChannel = null;
            }
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }
            if (Server != null)
            {
                Server.Dispose();
                Server = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
