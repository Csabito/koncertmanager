using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace koncertmanager
{
    //abstract internal class Koncerts
    //{
    //    private string knev, eloado, eloadas, hely, psize;
    //    private DateTime idopont;
    //    private int jegyar;

    //    public Koncerts(string knev, string eloado, string eloadas, string hely, string psize, DateTime idopont, int jegyar)
    //    {
    //        Knev = knev;
    //        Eloado = eloado;
    //        Eloadas = eloadas;
    //        Hely = hely;
    //        Psize = psize;
    //        Idopont = idopont;
    //        Jegyar = jegyar;
    //    }

    //    public string Knev { get => knev; set => knev = value; }
    //    public string Psize { get => psize; set => psize = value; }
    //    public string Eloado { get => eloado; set => eloado = value; }
    //    public string Eloadas { get => eloadas; set => eloadas = value; }
    //    public string Hely { get => hely; set => hely = value; }
    //    public DateTime Idopont { get => idopont; set => idopont = value; }
    //    public int Jegyar { get => jegyar; set => jegyar = value; }

        
    //}
    //internal class KoncertFeldolg : Koncerts
    //{
    //    public KoncertFeldolg(string knev, string eloado, string eloadas, string hely, string psize, DateTime idopont, int jegyar)
    //       : base(knev, eloado, eloadas, hely, psize, idopont, jegyar) 
    //    {
            
    //    }
    //    public static KoncertFeldolg FromFileString(string line)
    //    {
    //        var split = line.Split(';');
    //        return new KoncertFeldolg(split[0], split[1], split[2], split[3], split[4], DateTime.Parse(split[5]), int.Parse(split[6]));
    //    }
    //    public string ToFileString()
    //    {
    //        return $"{Knev};{Eloado};{Eloadas};{Hely};{Psize};{Idopont:yyyy-MM-dd HH:mm};{Jegyar}";
    //    }

    //}
    abstract class Koncert
    {
        private string knev, hely, psize;
        private DateTime idopont;
        public string Knev { get => knev; set => knev = value; }
        public string Hely { get => hely; set => hely = value; }
        public string Psize { get => psize; set => psize = value; }
        public DateTime Idopont { get => idopont; set => idopont = value; }

        public Koncert(string knev, string hely, string psize, DateTime idopont)
        {
            Knev = knev;
            Hely = hely;
            Psize = psize;
            Idopont = idopont;
        }

        public override string ToString()
        {
            return $"{Knev};{Hely};{Psize};{Idopont:yyyy-MM-dd HH:mm}";
        }

    }
    class Eloadasok : Koncert
    {
        private string eloado, eloadas;
        private int jegyar;
        public string Eloado { get => eloado; set => eloado = value; }
        public string Eloadas { get => eloadas; set => eloadas = value; }
        public int Jegyar { get => jegyar; set => jegyar = value; }

        public Eloadasok(string knev, string hely, string psize, DateTime idopont, string eloado, string eloadas, int jegyar) : base(knev, hely, psize, idopont)
        {
            Eloado = eloado;
            Eloadas = eloadas;
            Jegyar = jegyar;
        }
        public override string ToString()
        {
            return $"{base.ToString()};{Eloado};{Eloadas};{Jegyar}";
        }
    }
    class KoncertManager
    {
        private List<Koncert> koncert = new List<Koncert>();
        public void KoncertBeolvas()
        {
            koncert.Clear();

            if (System.IO.File.Exists("concerts.txt"))
            {
                System.IO.File.ReadAllLines("concerts.txt").ToList().ForEach(line =>
                {
                    var split = line.Split(';');
                    koncert.Add(new Eloadasok(split[0], split[1], split[2], DateTime.Parse(split[3]), split[4], split[5], int.Parse(split[6])));
                });
            }
            else
            {
                System.IO.File.Create("concerts.txt").Close();
            }
            
        }

        public void UjKoncert(string nev, string hely, string psize, DateTime idopont, string eloado, string eloadas, int jegyar)
        {
           
            koncert.Add(new Eloadasok(nev, hely, psize, idopont, eloado, eloadas, jegyar));

            System.IO.StreamWriter sw = new System.IO.StreamWriter("concerts.txt", append: true);
            sw.WriteLine($"{nev};{hely};{psize};{idopont};{eloado};{eloadas};{jegyar}");
            sw.Close();
        }

        // Expose read-only access to the loaded concerts
        public IReadOnlyList<Koncert> GetAllKoncert()
        {
            return koncert.AsReadOnly();
        }

        // Fill a ListView (assumed to have appropriate columns) with the concerts
        public void FillListView(ListView listView)
        {
            if (listView == null) return;

            listView.BeginUpdate();
            try
            {
                listView.Items.Clear();

                foreach (var k in koncert)
                {
                    var e = k as Eloadasok;
                    string eloado = e?.Eloado ?? string.Empty;
                    string eloadas = e?.Eloadas ?? string.Empty;
                    string jegyar = e != null ? e.Jegyar.ToString() : string.Empty;

                    var lvItem = new ListViewItem(new[]
                    {
                        k.Knev,
                        k.Hely,
                        k.Psize,
                        k.Idopont.ToString("yyyy-MM-dd"),
                        eloado,
                        eloadas,
                        jegyar
                    });

                    listView.Items.Add(lvItem);
                }
            }
            finally
            {
                listView.EndUpdate();
            }
        }

    }
}
