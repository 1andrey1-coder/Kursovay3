using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Rofl> Rofls { get; set; } = new List<Rofl>();
}
