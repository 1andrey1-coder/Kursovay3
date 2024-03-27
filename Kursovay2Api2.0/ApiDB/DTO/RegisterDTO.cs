using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDB.DTO
{
     public class RegisterDTO
     {
          public int Id { get; set; }

          public string? Login { get; set; }

          public string? Password { get; set; }

        public string? Mail { get; set; }

        public int RoleId { get; set; }
     }
    
}
