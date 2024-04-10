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

        public string? TegId { get; set; }

        public string? RoflStartId { get; set; }

        public string? RoflEndId { get; set; }

        public string? RoflName { get; set; }

        public string? RoflOpisanie { get; set; }

        public string? RoflStatusId { get; set; }

        public string? RoflGenreId { get; set; }

        public DateTime? RoflDateTime { get; set; }

        public byte[]? RoflImage { get; set; }

    }
}
