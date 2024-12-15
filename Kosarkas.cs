using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROJEKAT
{
    internal class Kosarkas : INotifyPropertyChanged
    {
        string jmbg;
        string ime;
        string prezime;
        string nacionalnost;
        int brojDresa;
        int brojUtakmica;
        int brojPoena;
        string slika;
        string pozicija;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public Kosarkas()
        {
            this.jmbg = "";
            this.ime = "";
            this.prezime = "";
            this.pozicija = "";
            this.nacionalnost = "";
            this.brojDresa = 0;
            this.brojUtakmica = 0;
            this.brojPoena = 0;
            this.slika = "";
        }

        public Kosarkas(string jmbg, string ime, string prezime, string pozicija, string nacionalnost, int broj_dresa, int broj_utakmica, int broj_poena)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.pozicija = pozicija;
            this.nacionalnost = nacionalnost;
            this.brojDresa = broj_dresa;
            this.brojUtakmica = broj_utakmica;
            this.brojPoena = broj_poena;
            this.slika="";
        }

        public Kosarkas(string jmbg, string ime, string prezime, string pozicija, string nacionalnost, int broj_dresa, int broj_utakmica, int broj_poena, string slika)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.pozicija = pozicija;
            this.nacionalnost = nacionalnost;
            this.brojDresa = broj_dresa;
            this.brojUtakmica = broj_utakmica;
            this.brojPoena = broj_poena;
            this.slika = "/Images/" + slika;
        }

        public string Slika { get { return this.slika; } 
            set 
            { 
                if(this.slika != value)
                { 
                    this.slika = value; 
                    this.NotifyPropertyChanged("Slika");
                }
            }
        }
            

        public string Jmbg { get {  return this.jmbg; }
            set
            {
                if (this.jmbg != value)
                {
                    this.jmbg = value;
                    this.NotifyPropertyChanged("Jmbg");
                }
            }
        }

        public string Pozicija
        {
            get { return this.pozicija; }
            set
            {
                if (this.pozicija != value)
                {
                    this.pozicija = value;
                    this.NotifyPropertyChanged("Pozicija");
                }
            }
        }

        public string Ime { get { return this.ime;}
            set
            {
                if (this.ime != value)
                {
                    this.ime = value;
                    this.NotifyPropertyChanged("Ime");
                }
            }
        }

        public string Prezime { get {  return this.prezime;}
            set
            {
                if (this.prezime != value)
                {
                    this.prezime = value;
                    this.NotifyPropertyChanged("Prezime");
                }
            }
        }

        public string Nacionalnost { get { return this.nacionalnost; }
            set
            {
                if (this.nacionalnost != value)
                {
                    this.nacionalnost = value;
                    this.NotifyPropertyChanged("Nacionalnost");
                }
            }
        }

        public int BrojPoena { get { return this.brojPoena; }
            set
            {
                if (this.brojPoena != value)
                {
                    this.brojPoena = value;
                    this.NotifyPropertyChanged("BrojPoena");
                }
            }
        }

        public int BrojDresa
        {
            get { return this.brojDresa; }
            set
            {
                if (this.brojDresa != value)
                {
                    this.brojDresa = value;
                    this.NotifyPropertyChanged("BrojDresa");
                }
            }
        }
        public int BrojUtakmica
        {
            get { return this.brojUtakmica; }
            set
            {
                if (this.brojUtakmica != value)
                {
                    this.brojUtakmica = value;
                    this.NotifyPropertyChanged("BrojUtakmica");
                }
            }
        }

        public string toString()
        {
            string[] delovi = this.slika.Split("/");
            //MessageBox.Show(delovi[1]);
            //MessageBox.Show(delovi[2]);
            string[] delovi2 = delovi[2].Split("\\");
            return this.jmbg + "," + this.ime + "," + this.prezime + "," + this.pozicija + "," + this.nacionalnost + ", " + this.brojDresa + ", " + this.brojUtakmica + ", " + this.brojPoena + "," + delovi2[delovi2.Length-1];
        }
    }
}
