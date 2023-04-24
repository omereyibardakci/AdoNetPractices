using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetPracticesDatabase
{
    internal class LoginTable
    {
        public int id { get; set; }
        public string kullaniciAdi { get; set; }
        public int sifre { get; set; }
        public string yetki { get; set; }
    }
}
