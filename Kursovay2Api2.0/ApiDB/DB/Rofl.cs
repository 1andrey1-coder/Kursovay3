using System;
using System.Collections.Generic;

namespace ApiDB.DB;

public partial class Rofl
{
    public int RoflId { get; set; }

    public int? TegId { get; set; }

    public int? RoflStartId { get; set; }

    public int? RoflEndId { get; set; }

    public string? RoflName { get; set; }

    public string? RoflOpisanie { get; set; }

    public string? RoflMinOpisanie { get; set; }

    public int? RoflStatusId { get; set; }

    public int? RoflGenreId { get; set; }

    public DateTime? RoflDateTime { get; set; }

    //public byte[]? RoflImage { get; set; }

    public virtual End? RoflEnd { get; set; }

    public virtual Genre? RoflGenre { get; set; }

    public virtual Start? RoflStart { get; set; }

    public virtual Status? RoflStatus { get; set; }

    public virtual Teg? Teg { get; set; }
}
