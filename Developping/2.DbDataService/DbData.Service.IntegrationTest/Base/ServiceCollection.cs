using Xunit;

namespace DbData.Service.IntegrationTest.Base
{
    [CollectionDefinition(TestCollections.ServiceIntegration)]
    public class ServiceCollection : ICollectionFixture<TestServerHosting>
    {
    }
}
