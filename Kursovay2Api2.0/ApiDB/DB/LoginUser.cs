using System;
using System.Collections.Generic;
using ApiDB.DB;

namespace ApiDB;

public partial class LoginUser
{
    public int LoginId { get; set; }

    public string? LoginName { get; set; }

    public string? LoginPassword { get; set; }

    public int? RoleId { get; set; }

    public string? Mail { get; set; }

    public byte[]? LoginImage { get; set; }

    public virtual Role? Role { get; set; }
}
