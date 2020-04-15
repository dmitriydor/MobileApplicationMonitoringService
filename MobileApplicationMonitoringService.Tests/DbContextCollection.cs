using Xunit;

namespace MobileApplicationMonitoringService.Tests
{
    [CollectionDefinition("Database context")]
    public class DbContextCollection: IClassFixture<DbContextFixture>
    {
        
    }
}