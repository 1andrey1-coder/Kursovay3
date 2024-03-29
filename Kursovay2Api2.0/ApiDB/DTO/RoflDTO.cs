﻿using System;
using System.Collections.Generic;
using ApiDB.DB;

namespace ApiDB;

public class RoflDTO
{
    public int RoflId { get; set; }

    public int? TegId { get; set; }

    public int? RoflStartId { get; set; }

    public int? RoflEndId { get; set; }

    public string? RoflName { get; set; }

    public string? RoflOpisanie { get; set; }

    public int? RoflStatusId { get; set; }

    public int? RoflGenreId { get; set; }

    public DateTime? RoflDateTime { get; set; }

    public byte[]? RoflImage { get; set; }

}
