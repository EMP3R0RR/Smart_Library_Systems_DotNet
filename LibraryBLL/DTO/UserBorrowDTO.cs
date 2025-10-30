using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.DTO
{
    public class UserBorrowDTO : BorrowDTO
    {
        public UserDTO User { get; set; }
        public StatusDTO Status { get; set; }
        public BookDTO Books { get; set; }


    }
}
