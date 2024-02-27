using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class Start
{
    public int StartId { get; set; }

    public string? StartName { get; set; }

    public virtual ICollection<Rofl> Rofls { get; set; } = new List<Rofl>();
}
