using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay2.API
{
    public class RoflDTO
    {
        public int RoflId { get; set; }

        //public string? TegId { get; set; }

        //public string? RoflStartId { get; set; }

        //public int? RoflEndId { get; set; }
        //public string? RoflEnd { get; set; }

        //public string? RoflName { get; set; }

        //public string? RoflOpisanie { get; set; }

        //public string? RoflStatusId { get; set; }

        //public string? RoflGenreId { get; set; }

        public DateTime? RoflDateTime { get; set; }

        public byte[]? RoflImage { get; set; }


        public int? TegId { get; set; }
        public string? Teg { get; set; }

        public int? RoflStartId { get; set; }
        public string? RoflStart { get; set; }

        public int? RoflEndId { get; set; }
        public string? RoflEnd { get; set; }

        public string? RoflName { get; set; }
        public string? RoflOpisanie { get; set; }
        public string? RoflMinOpisanie { get; set; }

        public int? RoflStatusId { get; set; }
        public string? RoflStatus { get; set; }

        public int? RoflGenreId { get; set; }
        public string? RoflGenre { get; set; }


    }
}
