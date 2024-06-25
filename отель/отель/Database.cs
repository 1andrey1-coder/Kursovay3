using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using отель.DB;

namespace отель
{
    public class Database
    {
        internal static object LoginTbls;
        private static _3kursContext db;

        public static _3kursContext GetInstance()
        {
            if(db == null)
                db = new ();
            return db;

                
        }

    }
}
