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
    /// Interaction logic for Izmena.xaml
    /// </summary>
    public partial class Izmena : Window
    {
        bool imaVecSlika = false;
        bool slika = false;
        Kosarkas k;
        private bool sadrzan = false;
        private MainWindow prvaStrana;
        private Kosarkas prosledjeniKosarkas = null;
        string stariJmbg;
        string staraSlika;
        bool izmenjen = false;
        string novaSlika;
        public Izmena()
        {
            InitializeComponent();
        }

        public Izmena(MainWindow prvaStr , string naziv)
        {
            InitializeComponent();
            prvaStrana = prvaStr;
            
            
            string imeSlike = naziv;
           foreach (Kosarkas k2 in prvaStrana.kosarkasi)
            {
                if(k2.Slika.Contains(imeSlike))
                {
                    prosledjeniKosarkas = k2;
                }
            }
            
            staraSlika = naziv;
            novaSlika = staraSlika;
            stariJmbg = prosledjeniKosarkas.Jmbg;
            jmbg.Text = prosledjeniKosarkas.Jmbg;
            ime.Text = prosledjeniKosarkas.Ime;
            prezime.Text = prosledjeniKosarkas.Prezime;
            pozicija.Text = prosledjeniKosarkas.Pozicija;
            nacionalnost.Text = prosledjeniKosarkas.Nacionalnost;
            brojDresa.Text = prosledjeniKosarkas.BrojDresa.ToString();
            brojPoena.Text = prosledjeniKosarkas.BrojPoena.ToString();
            brojUtakmica.Text = prosledjeniKosarkas.BrojUtakmica.ToString();
        }

        private void IzmeniIgraca(object sender, RoutedEventArgs e)
        {
            
            k = new Kosarkas(jmbg.Text, ime.Text, prezime.Text, pozicija.Text, nacionalnost.Text, Int32.Parse(brojDresa.Text), Int32.Parse(brojUtakmica.Text), Int32.Parse(brojPoena.Text),novaSlika);
            //MessageBox.Show(k.Slika);
            
            
            
            foreach (Kosarkas k2 in prvaStrana.kosarkasi)
            {
                if (k2.Jmbg.Equals(k.Jmbg))
                {
                    sadrzan = true;
                    
                }
                if (k2.Slika.Equals(k.Slika))
                {
                    imaVecSlika = true;
                }
            }

            try
            {
                
                if (staraSlika.Contains(novaSlika) || !imaVecSlika)
                {
                   
                    if (stariJmbg.Equals(jmbg.Text)|| ( k.Jmbg.Length == 13 && !sadrzan))
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

                                izmenjen = true;
                                //MessageBox.Show(k.toString());
                                IzmeniKosarkasa();
                                //sw.WriteLine(k.toString());
                                IzmeniTxt();

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
               
                    if(izmenjen)
                    this.Close();
                
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
                

                string[] delovi = selectedFileName.Split('\\');
                
                novaSlika = delovi[delovi.Length-1];
                
            }
        }
        private void IzmeniKosarkasa()
        {
            
            for(int i = 0;i<prvaStrana.kosarkasi.Count;i++)
            {
                if (prvaStrana.kosarkasi[i].Jmbg.Equals(stariJmbg))
                    prvaStrana.kosarkasi[i] = k;
            }
        }

        private void IzmeniTxt()
        {
            StreamWriter sw = null;
            try { 
            sw = new StreamWriter("Kosarkasi.txt");
                foreach(Kosarkas kos in prvaStrana.kosarkasi)
                {
                    sw.WriteLine(kos.toString());
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
            }
            finally { sw.Close(); }
            
        }
    }
}
