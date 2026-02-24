using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koncertmanager
{
    abstract internal class Koncert
    {
        private string knev, eloado, eloadas, hely;
        private DateTime idopont;
        private int jegyar;

        public Koncert(string knev, string eloado, string eloadas, string hely, DateTime idopont, int jegyar)
        {
            Knev = knev;
            Eloado = eloado;
            Eloadas = eloadas;
            Hely = hely;
            Idopont = idopont;
            Jegyar = jegyar;
        }

        public string Knev { get => knev; set => knev = value; }
        public string Eloado { get => eloado; set => eloado = value; }
        public string Eloadas { get => eloadas; set => eloadas = value; }
        public string Hely { get => hely; set => hely = value; }
        public DateTime Idopont { get => idopont; set => idopont = value; }
        public int Jegyar { get => jegyar; set => jegyar = value; }

        
    }
}
