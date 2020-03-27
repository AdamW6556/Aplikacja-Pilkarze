using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pilkarzproj
{
    class Archiwizacja
    {
        public static void ZapisPilkarzyDoPliku(string plik, Pilkarz[] pilkarze)
        {
            using (StreamWriter stream = new StreamWriter(plik))
            {
                foreach (var p in pilkarze)
                    stream.WriteLine(p.ToFileFormat());
                stream.Close();
            }
        }
        public static Pilkarz[] CzytajPilkarzyZPliku(string plik)
        {
            Pilkarz[] pilkarze = null;

            if (File.Exists(plik))
            {
                var plikPilkarze = File.ReadAllLines(plik);

                var x = plikPilkarze.Length;

                if (x > 0)
                {
                    pilkarze = new Pilkarz[x];

                    for (int i = 0; i < x; i++)
                    {
                        pilkarze[i] = Pilkarz.CreateFromString(plikPilkarze[i]);
                    }
                        
                    return pilkarze;
                }


            }
            return pilkarze;
        }




    }
}
