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
    /// Interaction logic for DodavanjeKluba.xaml
    /// </summary>
    public partial class DodavanjeKluba : Window
    {
        bool dodat = false;
        string selectedFileName = "";
        bool sadrzan = false;
        bool imaVecSlika = false;
        bool slika = false;
        private MainWindow prvaStrana;
        Klub k;
        public DodavanjeKluba()
        {
            InitializeComponent();
        }

        public DodavanjeKluba(MainWindow prvaStr)
        {
            InitializeComponent();
            prvaStrana = prvaStr;
        }

        private void DodajKlubb(object sender, RoutedEventArgs e)
        {
            if (selectedFileName.Equals(""))
            {
                MessageBox.Show("Unesite sliku.");
            }
            else
            {
                sadrzan = false;
                imaVecSlika = false;
                k = new Klub(Int32.Parse(ID.Text), IME.Text, GRAD.Text, selectedFileName);
                //MessageBox.Show(k.Slika);
                string[] delovi = k.Logo.Split("/");
                string[] delovi2 = delovi[2].Split("\\");
                k.Logo = delovi2[delovi2.Length - 1];
                foreach (Klub k2 in prvaStrana.klubovi)
                {
                    if (k2.Id.Equals(k.Id))
                    {
                        sadrzan = true;
                    }
                    if (k2.Logo.Contains(delovi2[delovi2.Length - 1]))
                    {
                        imaVecSlika = true;
                    }
                }

                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter("klubovi.txt", true);
                    if (sadrzan)
                    {
                        MessageBox.Show("Morate uneti neki id koji nije vec u tabeli klubova.");
                    }
                    else
                    {
                        if (imaVecSlika)
                        {
                            MessageBox.Show("Morate uneti sliku koja nije vec u tabeli klubova.");
                        }
                        else
                        {
                            if (!slika)
                            {
                                MessageBox.Show("Morate postaviti neku sliku za logo.");
                            }
                            else
                            {
                                if (IME.Text != "" && GRAD.Text != "" && ID.Text!="")
                                {
                                    sw.WriteLine(k.toString());
                                    dodat = true;
                                    MessageBox.Show("Uspesno dodat klub.");
                                }
                                else
                                {
                                    MessageBox.Show("Morate popuniti polja za id,grad i naziv.");
                                }
                            }
                        }
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
                        if (dodat)
                            this.Close();
                    }
                }
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
            }
        }
    }
}
