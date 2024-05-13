using Kursovay2.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovay2.Models
{
    public class SingleProfle
    {
        private static LoginUserDTO _user;

        public static LoginUserDTO User { get => _user; set => _user = value; }
    }
}
