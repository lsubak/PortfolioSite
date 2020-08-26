using Microsoft.Azure.Cosmos.Table;
namespace PortfolioSite.Internal.Database.AzureModels
{
    public class ContactMessageEntity : TableEntity
    {
        public ContactMessageEntity()
        {
        }

        public ContactMessageEntity(string timestamp, string firstName)
        {
            PartitionKey = timestamp;
            RowKey = firstName;
        }
    }
}
