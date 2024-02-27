using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class End
{
    public int EndId { get; set; }

    public string? EndName { get; set; }

    public virtual ICollection<Rofl> Rofls { get; set; } = new List<Rofl>();
}
