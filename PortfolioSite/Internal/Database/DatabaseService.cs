using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioSite.Internal.AppSettings;
using PortfolioSite.Internal.Database.AzureModels;
using PortfolioSite.Models;

namespace PortfolioSite.Internal.Database
{
    public class DatabaseService : IDatabaseService
    {
        private CloudStorageAccount _storageAccount;
        private CloudTableClient _tableClient;
        private CloudTable _contactMessagesTable;
        private CloudTable _errorsTable;

        private DatabaseSettings _settings;

        public DatabaseService(IOptions<DatabaseSettings> options)
        {
            _settings = options.Value;

            _storageAccount = CloudStorageAccount.Parse(_settings.TableStorageConnectionString);
            _tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            
            _contactMessagesTable = _tableClient.GetTableReference("ContactMessages");
            _contactMessagesTable.CreateIfNotExistsAsync();

            _errorsTable = _tableClient.GetTableReference("Errors");
            _errorsTable.CreateIfNotExistsAsync();
        }

        public void SaveContactMessage(ContactForm form)
        {
            var json = JsonConvert.SerializeObject(form);
            var item = new ContactMessageEntity(json);

            TableOperation op = TableOperation.Insert(item);

            _contactMessagesTable.Execute(op);
        }

        public void SaveErrorMessages(string message)
        {
            var item = new ErrorEntity(message);

            TableOperation op = TableOperation.Insert(item);

            _errorsTable.Execute(op);
        }
    }
}
