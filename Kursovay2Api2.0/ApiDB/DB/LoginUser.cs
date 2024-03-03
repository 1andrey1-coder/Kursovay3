using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class LoginUser
{
    public int LoginId { get; set; }

    public string? LoginName { get; set; }

    public string? LoginPassword { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
