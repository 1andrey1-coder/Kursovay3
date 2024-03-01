using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDB.DTO
{
    public  class UserLoginDTO
    {
        public int LoginId { get; set; }

        public string? Login { get; set; }

        public string? password { get; set; }
    }
}
