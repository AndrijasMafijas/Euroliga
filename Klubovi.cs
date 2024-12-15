using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKAT
{
    class Klubovi:INotifyPropertyChanged
    {
        string naslov;
        ObservableCollection<Klub> sviKlubovi=new ObservableCollection<Klub>();

        public event PropertyChangedEventHandler PropertyChanged;
        public Klubovi() { }

        public Klubovi(string naslov)
        {
            this.naslov = naslov;
        }

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
        public string Naslov
        {
            get { return this.naslov; }
            set
            {
                if (this.naslov != value)
                {
                    this.naslov = value;
                    this.NotifyPropertyChanged("Naslov");
                }
            }
        }
        public ObservableCollection<Klub> SviKlubovi
        {
            get { return this.sviKlubovi; }
            set
            {
                if (this.sviKlubovi != value)
                {
                    this.sviKlubovi = value;
                    this.NotifyPropertyChanged("SviKlubovi");
                }
            }
        }
    }
}
