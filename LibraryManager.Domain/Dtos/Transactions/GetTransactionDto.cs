using System;

namespace LibraryManager.Domain.Dtos.Transactions
{
    public class GetTransactionDto
    {
        public long? BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ReturnDate { get; set; }
        public DateTimeOffset? ReturnedAt { get; set; }
        public string StudentName { get; set; }
        public long TransactionId { get; set; }
    }
}
