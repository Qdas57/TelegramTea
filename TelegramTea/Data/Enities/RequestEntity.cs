using System;

namespace TelegramTea.Data.Enities
{
    public class RequestEntity
    {
        public long Id { get; set; }

        public string RequestType { get; set; }

        public DateTime Date { get; set; }

        public int ChatId { get; set; }

        public string? FirstName { get; set; }
    }
}
