using Microsoft.Azure.Cosmos.Table;

namespace PortfolioSite.Internal.Database.AzureModels
{
    public class ErrorEntity : TableEntity
    {
        public ErrorEntity()
        {
        }

        public ErrorEntity(string timestamp, string firstName)
        {
            PartitionKey = timestamp;
            RowKey = firstName;
        }
    }
}
