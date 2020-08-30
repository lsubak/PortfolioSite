using Microsoft.Azure.Cosmos.Table;
using System;

namespace PortfolioSite.Internal.Database.AzureModels
{
    public class ErrorEntity : TableEntity
    {
        public string Message { get; set; }

        public ErrorEntity()
        {
        }

        public ErrorEntity(string message)
        {
            PartitionKey = string.Empty;
            RowKey = DateTime.UtcNow.ToString();
            Message = message;
        }
    }
}
