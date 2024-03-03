using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Rofl> Rofls { get; set; } = new List<Rofl>();
}
