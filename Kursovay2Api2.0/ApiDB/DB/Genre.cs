using System;
using System.Collections.Generic;
using ApiDB.DB;

namespace ApiDB;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public virtual ICollection<Rofl> Rofls { get; set; } = new List<Rofl>();
}
