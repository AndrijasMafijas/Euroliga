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
    /// Interaction logic for IzmenaKluba.xaml
    /// </summary>
    public partial class IzmenaKluba : Window
    {
        bool imaVecSlika = false;
        bool slika = false;
        Klub k;
        private bool sadrzan = false;
        private MainWindow prvaStrana;
        private Klub prosledjeniKlub = null;
        string stariId;
        string stariLogo;
        bool izmenjen = false;
        string noviLogo;
        string selectedFileName;
        bool dodat = false;
        public IzmenaKluba()
        {
            InitializeComponent();
        }

        public IzmenaKluba(MainWindow prvaStr, string naziv)
        {
            InitializeComponent();
            prvaStrana = prvaStr;


            string imeSlike = naziv;
            foreach (Klub k2 in prvaStrana.klubovi)
            {
                if (k2.Logo.Contains(imeSlike))
                {
                    prosledjeniKlub = k2;
                }
            }

            stariLogo = prosledjeniKlub.Logo;
            noviLogo = stariLogo;
            stariId = prosledjeniKlub.Id.ToString();
            ID.Text = prosledjeniKlub.Id.ToString();
            IME.Text = prosledjeniKlub.Naziv;
            GRAD.Text = prosledjeniKlub.Grad;
            selectedFileName = prosledjeniKlub.Logo;
        }

        private void DodajKlubb(object sender, RoutedEventArgs e)
        {
            
                k = new Klub(Int32.Parse(ID.Text), IME.Text, GRAD.Text, noviLogo);
                //MessageBox.Show(k.Slika);
                
                foreach (Klub k2 in prvaStrana.klubovi)
                {
                    if (k2.Id.Equals(k.Id))
                    {
                        sadrzan = true;
                    }
                    if (k2.Logo.Contains(k.Logo))
                    {
                        imaVecSlika = true;
                    }
                }

            try
            {
                if(!sadrzan || stariId == ID.Text)
                {
                    if(stariLogo.Contains(noviLogo) || !imaVecSlika)
                    {
                        if(ID.Text.Equals("") || IME.Text.Equals("") || GRAD.Text.Equals(""))
                        {
                            MessageBox.Show("Morate popuniti sva polja");
                        }
                        else
                        {
                            dodat = true;
                            IzmeniKlub();
                            IzmeniTxtKlub();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ne mozete da izaberete tu sliku!");
                    }
                }
                else
                {
                    MessageBox.Show("Taj id vec postoji!");
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                Console.WriteLine(exp.StackTrace);
            }
            finally
            {
                //sw.Close();
                if (dodat)
                    this.Close();
            }


          
            
        }

        private void izaberiLogo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Napomena: Slike se moraju cuvati u folderu Images.");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an Image";
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            // Show OpenFileDialog and check if user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the selected file's path
                selectedFileName = openFileDialog.FileName;

                // Create a BitmapImage
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();

                slika = true;

                string[] delovi = selectedFileName.Split('\\');
                noviLogo = delovi[delovi.Length - 1];
            }
        }

        private void IzmeniKlub()
        {

            for (int i = 0; i < prvaStrana.klubovi.Count; i++)
            {
                if (prvaStrana.klubovi[i].Id.ToString().Equals(stariId))
                    prvaStrana.klubovi[i] = k;
            }
        }

        private void IzmeniTxtKlub()
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter("klubovi.txt");
                foreach (Klub kl in prvaStrana.klubovi)
                {
                    sw.WriteLine(kl.toString());
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
