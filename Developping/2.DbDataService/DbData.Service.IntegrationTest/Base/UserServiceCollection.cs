using Xunit;

namespace DbData.Service.IntegrationTest.Base
{
    [CollectionDefinition(TestCollections.UserServiceIntegration)]
    public class UserServiceCollection : ICollectionFixture<TestServerHosting>
    {
    }
}
