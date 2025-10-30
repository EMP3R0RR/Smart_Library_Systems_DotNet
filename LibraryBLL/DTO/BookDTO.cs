using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.DTO
{
    public class BookDTO
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public System.DateTime CreatedAt { get; set; }


    }
}
