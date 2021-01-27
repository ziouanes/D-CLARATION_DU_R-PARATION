using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication1
{
    public class Reaparation 
    {
        public int id { get; set; }
        public string Nom { get; set; }
        public int id_vehecule { get; set; }
        public string Marque { get; set; }
        public string Immatriculation { get; set; }
        public string Carburant { get; set; }
        public DateTime Date { get; set; }
        public int kilometrage { get; set; }

    }
}
