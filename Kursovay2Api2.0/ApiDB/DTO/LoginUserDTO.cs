using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public class LoginUserDTO
{
    public int LoginId { get; set; }

    public string? LoginName { get; set; }

    public string? LoginPassword { get; set; }

    public int? RoleId { get; set; }

    
}
