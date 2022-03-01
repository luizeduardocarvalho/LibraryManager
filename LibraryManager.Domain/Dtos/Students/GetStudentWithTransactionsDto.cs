using LibraryManager.Domain.Dtos.Transactions;
using System.Collections.Generic;

namespace LibraryManager.Domain.Dtos.Students
{
    public class GetStudentWithTransactionsDto
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public string TeacherName { get; set; }
        public IEnumerable<GetTransactionDto> Transactions { get; set; }
    }
}
