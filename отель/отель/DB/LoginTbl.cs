using System;
using System.Collections.Generic;

namespace отель.DB;

public partial class LoginTbl
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
