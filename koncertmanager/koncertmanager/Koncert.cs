using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koncertmanager
{
<<<<<<< Updated upstream
    abstract internal class Koncert
=======
    abstract class Koncert
>>>>>>> Stashed changes
    {
        private string knev, eloado, eloadas, hely, psize;
        private DateTime idopont;
        private int jegyar;

        public Koncert(string knev, string eloado, string eloadas, string hely, string psize, DateTime idopont, int jegyar)
        {
            Knev = knev;
            Eloado = eloado;
            Eloadas = eloadas;
            Hely = hely;
            Psize = psize;
            Idopont = idopont;
            Jegyar = jegyar;
        }

        public string Knev { get => knev; set => knev = value; }
        public string Psize { get => psize; set => psize = value; }
        public string Eloado { get => eloado; set => eloado = value; }
        public string Eloadas { get => eloadas; set => eloadas = value; }
        public string Hely { get => hely; set => hely = value; }
        public DateTime Idopont { get => idopont; set => idopont = value; }
        public int Jegyar { get => jegyar; set => jegyar = value; }

        
    }
    internal class KoncertFeldolg : Koncert
    {
        public KoncertFeldolg(string knev, string eloado, string eloadas, string hely, string psize, DateTime idopont, int jegyar)
           : base(knev, eloado, eloadas, hely, psize, idopont, jegyar) 
        {
            
        }
        public static KoncertFeldolg FromFileString(string line)
        {
            var split = line.Split(';');
            return new KoncertFeldolg(split[0], split[1], split[2], split[3], split[4], DateTime.Parse(split[5]), int.Parse(split[6]));
        }
        public string ToFileString()
        {
            return $"{Knev};{Eloado};{Eloadas};{Hely};{Psize};{Idopont:yyyy-MM-dd HH:mm};{Jegyar}";
        }

    }
}
