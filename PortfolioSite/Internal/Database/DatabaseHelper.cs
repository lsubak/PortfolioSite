using Microsoft.Azure.Cosmos.Table;

namespace PortfolioSite.Internal.Database
{
    public class DatabaseHelper
    {
        private string _connectionString;
        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;
        private CloudTable _contactMessagesTable;
        private CloudTable _errorsTable;

        public DatabaseHelper()
        {
            _storageAccount = CloudStorageAccount.Parse(_connectionString);
            _tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            
            _contactMessagesTable = _tableClient.GetTableReference("ContactMessages");
            _contactMessagesTable.CreateIfNotExistsAsync();

            _errorsTable = _tableClient.GetTableReference("Errors");
            _errorsTable.CreateIfNotExistsAsync();
        }

        public void SaveContactMessage()
        {

        }

        public void SaveErrorMessages()
        {

        }
    }
}
