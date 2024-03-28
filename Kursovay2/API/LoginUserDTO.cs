using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay2.API
{
    public class LoginUserDTO
    {
        public int LoginId { get; set; }

        public string? LoginName { get; set; }

        public string? LoginPassword { get; set; }

        public string? Mail { get; set; }

        public int RoleId { get; set; }


    }
}
