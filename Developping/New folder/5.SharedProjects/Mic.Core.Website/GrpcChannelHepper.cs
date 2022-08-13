using Grpc.Core;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace Mic.Core.Website
{
    public class GrpcChannelHepper
    {
        public GrpcChannel CreateDDataChanel(string accessToken)
        {
            GrpcChannel channel;
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var credentials = CallCredentials.FromInterceptor((context, metadata) =>
                {
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        metadata.Add("Authorization", $"Bearer {accessToken}");
                    }
                    return Task.CompletedTask;
                });
                channel = GrpcChannel.ForAddress(UrlList.DynamicDataService, new GrpcChannelOptions
                {
                    Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
                });
            }
            else
            {
                channel = GrpcChannel.ForAddress(UrlList.DynamicDataService);
            }
            return channel;
        }

        public GrpcChannel CreateDbDataChanel(string accessToken)
        {
            GrpcChannel channel;
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                var credentials = CallCredentials.FromInterceptor((context, metadata) =>
                {
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        metadata.Add("Authorization", $"Bearer {accessToken}");
                    }
                    return Task.CompletedTask;
                });
                channel = GrpcChannel.ForAddress(UrlList.DbDataService, new GrpcChannelOptions
                {
                    Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
                });
            }
            else
            {
                channel = GrpcChannel.ForAddress(UrlList.DbDataService);
            }
            return channel;
        }
    }
}
