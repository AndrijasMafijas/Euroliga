using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace PROJEKAT
{
    /// <summary>
    /// Interaction logic for Dodavanje.xaml
    /// </summary>
    public partial class Dodavanje : Window
    {
        bool imaVecSlika = false;
        bool slika = false;
        Kosarkas k;
        private bool sadrzan = false;
        private MainWindow prvaStrana;
        bool dodat = false;
        public Dodavanje()
        {
            InitializeComponent();
        }

        public Dodavanje(MainWindow prvaStr)
        {
            InitializeComponent();
            prvaStrana = prvaStr;
        }

        private void DodajIgraca(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter("Kosarkasi.txt", true);
                if (slika && !imaVecSlika)
                {
                    if (k.Jmbg.Length == 13 && !sadrzan)
                    {
                        if (k.Pozicija.Equals("Sve pozicije"))
                        {
                            MessageBox.Show("Morate odabrati poziciju za igraca!");
                        }
                        else
                        {
                            if (k.Ime.Equals("") || k.Prezime.Equals("") || k.Nacionalnost.Equals("") || k.BrojUtakmica == -1 || k.BrojPoena == -1 || k.BrojDresa == -1)
                            {
                                MessageBox.Show("Morate popuniti sva polja (Ime,Prezime,Nacionalnost,Broj Utakmica,Broj Dresa,Broj Poena).");
                            }
                            else
                            {
                                //MessageBox.Show(k.toString());
                                sw.WriteLine(k.toString());
                                dodat = true;
                                MessageBox.Show("Uspesno dodat novi igrac");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Neispravan jmbg!");
                    }
                }
                else 
                {
                    MessageBox.Show("Morate dodati ispravnu sliku!");
                }
                
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    if(dodat)
                    this.Close();
                }
            }
        }

        private void izaberiSliku(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Napomena: Slike se moraju cuvati u folderu Images.");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Image";
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            // Show OpenFileDialog and check if user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file's path
                string selectedFileName = openFileDialog.FileName;

                // Create a BitmapImage
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                
                slika = true;
                k = new Kosarkas(jmbg.Text,ime.Text,prezime.Text,pozicija.Text,nacionalnost.Text,Int32.Parse(brojDresa.Text),Int32.Parse(brojUtakmica.Text),Int32.Parse(brojPoena.Text),selectedFileName);
                //MessageBox.Show(k.Slika);
                string[] delovi = k.Slika.Split("/");
                string[] delovi2 = delovi[2].Split("\\");
                foreach (Kosarkas k2 in prvaStrana.kosarkasi)
                {
                    if (k2.Jmbg.Equals(k.Jmbg))
                    {
                        sadrzan = true;
                        break;
                    }
                    if (k2.Slika.Contains(delovi2[delovi2.Length-1]))
                    {
                        imaVecSlika = true;
                        break;
                    }
                }
            }
        }
    }
}
