using System;
using System.Collections.Generic;
namespace ApiDB.DTO
{
    public class LoginUserDTO
    {
        public int LoginId { get; set; }

        public string? LoginName { get; set; }

        public string? LoginPassword { get; set; }

        public int? RoleId { get; set; }


    }
}


