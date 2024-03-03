using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class Teg
{
    public int TegId { get; set; }

    public string? TegName { get; set; }

    public virtual ICollection<Rofl> Rofls { get; set; } = new List<Rofl>();
}
