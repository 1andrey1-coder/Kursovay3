using System;
using System.Collections.Generic;
using ApiDB.DB;

namespace ApiDB;

public class RoflDTO
{
    public int RoflId { get; set; }

    public string? TegId { get; set; }

    public string? RoflStartId { get; set; }

    public int? RoflEndId { get; set; }
    public string? RoflEnd { get; set; }

    public string? RoflName { get; set; }

    public string? RoflOpisanie { get; set; }

    public string? RoflStatusId { get; set; }

    public string? RoflGenreId { get; set; }

    public DateTime? RoflDateTime { get; set; }

    //public byte[]? RoflImage { get; set; }

}
