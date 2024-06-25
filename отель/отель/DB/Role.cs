using System;
using System.Collections.Generic;

namespace отель.DB;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<LoginTbl> LoginTbls { get; set; } = new List<LoginTbl>();
}
