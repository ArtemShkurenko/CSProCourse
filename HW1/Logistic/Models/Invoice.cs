using System;


namespace Logistic.ConsoleClient.Models
{
    internal class Invoice
    {
        public Guid Id { get; set; }
        public string RecipientAddress { get; set; }
        public string SenderAddress { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string SenderPhoneNumber { get; set; }

    }
}
