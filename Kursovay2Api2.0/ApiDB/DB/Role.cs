using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<LoginUser> LoginUsers { get; set; } = new List<LoginUser>();
}
