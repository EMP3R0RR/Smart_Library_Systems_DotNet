using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.DTO
{
   public class BorrowRequestDTO
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
    }
}
