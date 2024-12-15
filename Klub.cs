using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKAT
{
    class Klub : INotifyPropertyChanged
    {
        int id;
        string naziv;
        string grad;
        string logo;
        public Klub() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
        public Klub(int id, string naziv, string grad, string logo)
        {
            this.id = id;
            this.naziv = naziv;
            this.grad = grad;
            this.logo = "/Images/"+logo;
        }

        public string Naziv
        {
            get { return this.naziv; }
            set
            {
                if (this.naziv != value)
                {
                    this.naziv = value;
                    this.NotifyPropertyChanged("Naziv");
                }
            }
        }

        public string Logo
        {
            get { return this.logo; }
            set
            {
                if (this.logo != value)
                {
                    this.logo = value;
                    this.NotifyPropertyChanged("Logo");
                }
            }
        }

        public string Grad
        {
            get { return this.grad; }
            set
            {
                if (this.grad != value)
                {
                    this.grad = value;
                    this.NotifyPropertyChanged("Grad");
                }
            }
        }

        public int Id
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.NotifyPropertyChanged("Id");
                }
            }


        }

        public string toString()
        {
            string[] delovi = logo.Split('/');
            return id + "," + naziv + "," + grad + "," + delovi[delovi.Length - 1];
        }
    }
}
