using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.DTO
{
    public class BorrowDTO
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public System.DateTime BorrowDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public decimal FineAmount { get; set; }
        public int StatusID { get; set; }
    }
}
