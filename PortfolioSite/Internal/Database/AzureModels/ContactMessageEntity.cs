using Microsoft.Azure.Cosmos.Table;
using System;

namespace PortfolioSite.Internal.Database.AzureModels
{
    public class ContactMessageEntity : TableEntity
    {
        public string Message { get; set; }

        public ContactMessageEntity()
        {
        }

        public ContactMessageEntity(string contactForm)
        {
            PartitionKey = string.Empty;
            RowKey = DateTime.UtcNow.ToString();
            Message = contactForm;
        }
    }
}
