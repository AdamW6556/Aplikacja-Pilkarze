using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pilkarzproj
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string plikArchiwizacji = "C:/Users/AdamW/Desktop/Pilkarzproj/archiwum.txt";
        public MainWindow()
        {
            TextBoxWithErrorProvider.BrushForAll = Brushes.Red;

            InitializeComponent();

            TextBoxWepImie.SetFocus();
        }

        private bool IsNotEmpty(TextBoxWithErrorProvider tb)
        {
            if (tb.Text.Trim() == "")
            {
                tb.SetError("Pole nie może być puste!");
                return false;
            }
            tb.SetError("");
            return true;
        }

        private void Clear()
        {
            
            TextBoxWepImie.Text = "";
            TextBoxWepNazwisko.Text = "";
            wagaslider.Value = 75;
            wiekslider.Value = 30;         
            button3.IsEnabled = false;
            button2.IsEnabled = false;
          
            ListBoxPilkarze.SelectedIndex = -1;
            TextBoxWepImie.SetFocus();
        }
        private void LoadPlayer(Pilkarz pilkarz)
        {
            TextBoxWepImie.Text = pilkarz.Imie;
            TextBoxWepNazwisko.Text = pilkarz.Nazwisko;
            wagaslider.Value = pilkarz.Waga;
            wiekslider.Value = pilkarz.Wiek;
            button3.IsEnabled = true;
            button2.IsEnabled = true;
            TextBoxWepImie.SetFocus();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          if (IsNotEmpty(TextBoxWepImie) & IsNotEmpty(TextBoxWepNazwisko))
          {
            
            var czyJuzJestNaLiscie = false;
            var biezacyPilkarz = new Pilkarz(TextBoxWepImie.Text.Trim(), TextBoxWepNazwisko.Text.Trim(), (uint)wiekslider.Value, (uint)wagaslider.Value);
            foreach (var p in ListBoxPilkarze.Items)
            {
                var pilkarz = p as Pilkarz;
                if (pilkarz.takisam(biezacyPilkarz))
                {
                    czyJuzJestNaLiscie = true;
                    break;
                }
            }
            if (!czyJuzJestNaLiscie)
            {               
                ListBoxPilkarze.Items.Add(biezacyPilkarz);
                Clear();
            }
            else
            {
                var dialog = MessageBox.Show($"Ten pilkarz już jest na liście {Environment.NewLine} Czy wyczyścić formularz?","Ok", MessageBoxButton.OKCancel);
                if (dialog == MessageBoxResult.OK)
                {
                    Clear();
                }
            }
          }                       
        }
      
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
         
     var dialogResult = MessageBox.Show($"Czy na pewno chcesz usunac dane {Environment.NewLine} {ListBoxPilkarze.SelectedItem}?", "Edycja", MessageBoxButton.YesNo);
           
            if (dialogResult == MessageBoxResult.Yes)
            {
                ListBoxPilkarze.Items.Remove(ListBoxPilkarze.SelectedItem);
            }
            Clear();
        }
        private void ListBoxPilkarze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (ListBoxPilkarze.SelectedIndex > -1)
            {
                LoadPlayer((Pilkarz)ListBoxPilkarze.SelectedItem);
            }

        }
       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
          if (IsNotEmpty(TextBoxWepImie) & IsNotEmpty(TextBoxWepNazwisko))
          {
          
          var biezacyPilkarz = new Pilkarz(TextBoxWepImie.Text.Trim(), TextBoxWepNazwisko.Text.Trim(), (uint)wiekslider.Value, (uint)wagaslider.Value);

                bool czyJuzJestNaLiscie = false;

            foreach (var p in ListBoxPilkarze.Items)
            {
                var pilkarz = p as Pilkarz;
                if (pilkarz.takisam(biezacyPilkarz))
                {
                    czyJuzJestNaLiscie = true;
                    break;
                }
            }
            if (!czyJuzJestNaLiscie)
            {

                var dialogResult = MessageBox.Show($"Czy na pewno chcesz zmienić dane  {Environment.NewLine} {ListBoxPilkarze.SelectedItem}?", "Edycja", MessageBoxButton.YesNo);
                Pilkarz selected = (Pilkarz)ListBoxPilkarze.SelectedItem;
                if (dialogResult == MessageBoxResult.Yes)
                {
                    int wybranyindeks = ListBoxPilkarze.Items.IndexOf(selected);
                    try
                    {
                        selected.Imie = TextBoxWepImie.Text;

                        selected.Nazwisko = TextBoxWepNazwisko.Text;

                        selected.Wiek = Convert.ToUInt32(wiekslider.Value);

                        selected.Waga = Convert.ToUInt32(wagaslider.Value);

                        ListBoxPilkarze.Items.RemoveAt(wybranyindeks);

                        ListBoxPilkarze.Items.Insert(wybranyindeks, selected);

                        ListBoxPilkarze.SelectedItem = selected;

                            Clear();
                    }
                    catch { }
                }
                
            }
            else
            {
                MessageBox.Show($"Ten pilkarz już jest na liście.", "Uwaga");
            }

          }
            

        }

        private void window_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = ListBoxPilkarze.Items.Count;

            Pilkarz[] pilkarze = null;

            if (n > 0)
            {
                pilkarze = new Pilkarz[n];
                int i = 0;


                foreach (var o in ListBoxPilkarze.Items)
                {
                    pilkarze[i++] = o as Pilkarz;
                }

                Archiwizacja.ZapisPilkarzyDoPliku(plikArchiwizacji, pilkarze);
            }
        }

        private void window_loaded(object sender, RoutedEventArgs e)
        {
            var pilkarze = Archiwizacja.CzytajPilkarzyZPliku(plikArchiwizacji);

            if (pilkarze != null)

                foreach (var gracz in pilkarze)
                {
                    ListBoxPilkarze.Items.Add(gracz);
                }
        }
    }
}
