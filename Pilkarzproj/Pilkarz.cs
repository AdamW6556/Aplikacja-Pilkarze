using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pilkarzproj
{
    class Pilkarz
    {
        #region wlasciwosci
        public string Imie { get; set; } = "";       //oznacza to samo co kod wyzej,//inicjacja wartoscia pusta
        public string Nazwisko { get; set; }

        public uint Wiek { get; set; }

        public uint Waga { get; set; }


        public Pilkarz(string imie, string nazwisko, uint wiek, uint waga)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Wiek = wiek;
            Waga = waga;
        }

        public Pilkarz(string imie, string nazwisko) : this(imie, nazwisko, 30,75) {



        }

        public override string ToString()
        {        
           return string.Format( $"{Imie} {Nazwisko}, lat: {Wiek.ToString()} waga: {Waga} kg");
        
        #endregion
        }


        public string ToFileFormat()
        {
            return $"{Nazwisko} {Imie} {Wiek} {Waga}";
        }

        public bool takisam(Pilkarz pilkarz)
        {
            if (pilkarz.Nazwisko != Nazwisko) return false;
            if (pilkarz.Imie != Imie) return false;
            if (pilkarz.Wiek != Wiek) return false;
            if (pilkarz.Waga != Waga) return false;
            return true;
        }
        public static Pilkarz CreateFromString(string sPilkarz)
        {
          
                string imie, nazwisko;
                uint wiek, waga;
                var pola = sPilkarz.Split(' ');
                if (pola.Length == 4)
                {
                    nazwisko = pola[1];

                    imie = pola[0];

                    wiek = uint.Parse(pola[2]);

                    waga = uint.Parse(pola[3]);

                    return new Pilkarz(imie, nazwisko, wiek, waga);
                }
         
            throw new Exception("Błędny format danych z pliku");
        }


    }
}
