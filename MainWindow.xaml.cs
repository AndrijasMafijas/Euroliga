using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using System.Windows.Documents.DocumentStructures;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace PROJEKAT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point startPoint = new Point();
        Kosarkas k = new Kosarkas();
        internal  List<Kosarkas> kosarkasi = new List<Kosarkas>();
        internal ObservableCollection<Klub> klubovi=new ObservableCollection<Klub>();
        internal Klubovi Svi=new Klubovi("Svi klubovi");
        internal ObservableCollection<Klubovi> sve=new ObservableCollection<Klubovi>();
        
        private Point clickPosition;
        public MainWindow()
        {
            InitializeComponent();
            osvezi();
        }

        private void Pretrazi_Click(object sender, RoutedEventArgs e)
        {
            List<Kosarkas> pretrazeni = new List<Kosarkas>();

            if (txtIme.Text.Equals("") && txtPrezime.Text.Equals("") && txtPozicija.Text.Equals("Sve pozicije"))
            {
                pretrazeni.AddRange(kosarkasi);
            }
            else
            {
                if (!txtPozicija.Text.Equals("Sve pozicije"))
                    foreach (Kosarkas k in kosarkasi)
                    {

                        if (k.Ime.ToLower().Contains(txtIme.Text.ToLower()) && k.Prezime.ToLower().Contains(txtPrezime.Text.ToLower()) && k.Pozicija.Contains(txtPozicija.Text))
                        {
                            pretrazeni.Add(k);
                        }

                    }
                else

                    foreach (Kosarkas k in kosarkasi)
                    {

                        if (k.Ime.ToLower().Contains(txtIme.Text.ToLower()) && k.Prezime.ToLower().Contains(txtPrezime.Text.ToLower()))
                        {
                            pretrazeni.Add(k);
                        }



                    }
            }
            ViewDataGrid.ItemsSource = pretrazeni.ToList();
        }

        private void Eksportuj_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter("Exportovani.csv");
                sw.WriteLine("Ime,Prezime,Pozicija,Broj Poena");


                List<Kosarkas> pretrazeni = ViewDataGrid.ItemsSource as List<Kosarkas>;
                


                foreach(var k in pretrazeni)
                {
                    sw.WriteLine(k.Ime + "," + k.Prezime + "," + k.Pozicija + "," + k.BrojPoena);
                }

                sw.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.StackTrace);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                Image image = e.OriginalSource as Image;

                if (image != null)
                {
                    ListViewItem listViewItem = FindAncestor<ListViewItem>(image);

                    if (listViewItem == null) return;

                    // Dodato: Proveri da li je ListViewItem već onemogućen
                    if (!listViewItem.IsEnabled)
                    {
                        return;
                    }

                    Kosarkas k = (Kosarkas)ListView1.ItemContainerGenerator.ItemFromContainer(listViewItem);

                    if (k == null) return;

                    DataObject dragData = new DataObject("myImageFormat", image.Source);

                    DragDrop.DoDragDrop(image, dragData, DragDropEffects.Move);

                    // Dodato: Onemogući ListViewItem nakon prevlačenja
                    listViewItem.IsEnabled = false;
                }
            }
        }
       

        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myImageFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }

        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myImageFormat"))
            {
               
                var imageSource = e.Data.GetData("myImageFormat") as ImageSource;
                if (imageSource != null)
                {
                    MenuItem mi1=new MenuItem();
                    mi1.Header = "Izbrisi igraca";
                    mi1.Click += Izbrisi;
                    MenuItem mi2 = new MenuItem();
                    mi2.Header = "Ukloni sliku";
                    mi2.Click += Ukloni;
                    ContextMenu cm=new ContextMenu();
                    cm.Items.Add(mi1);
                    cm.Items.Add(mi2 );
                    Image newImage = new Image
                    {
                        Source = imageSource,
                        Width = 110,
                        Height = 65,
                        ContextMenu= cm
                    };

                    Point dropPosition = e.GetPosition(Canvas);
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    newImage.VerticalAlignment = VerticalAlignment.Top;

                    // Dodajemo TranslateTransform kako bismo pratili pomeranje slike
                    TranslateTransform transform = new TranslateTransform();
                    newImage.RenderTransform = transform;

                    // Postavljamo početnu poziciju slike
                    transform.X = dropPosition.X;
                    transform.Y = dropPosition.Y;

                    // Dodajemo sliku u grid i dodeljujemo event handlere za pomeranje
                    
                    newImage.MouseLeftButtonDown += Image_MouseDown;
                    newImage.MouseMove += Image_MouseMove;
                    newImage.MouseLeftButtonUp += Image_MouseUp;
                    newImage.MouseDown += Image_MouseDown;
                    newImage.MouseUp += Image_MouseUp;
                    Canvas.Children.Add(newImage);
                    

                }

            }
        }
        private Image draggedImage;
        private Point startPoint1;
        private HashSet<object> droppedItems = new HashSet<object>();

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (slika != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(slika);  // Pozicija miša u odnosu na veliku sliku

                double offsetX = currentPoint.X - startPoint1.X;
                double offsetY = currentPoint.Y - startPoint1.Y;

                // Dimenzije velike slike (granice)
                double backgroundImageWidth = slika.ActualWidth;
                double backgroundImageHeight = slika.ActualHeight;

                // Dimenzije grid-a
                double gridWidth = Canvas.ActualWidth;
                double gridHeight = Canvas.ActualHeight;

                // Pocetne koordinate velike slike u gridu
                double imageLeft = (gridWidth - backgroundImageWidth) / 2;
                double imageTop = (gridHeight - backgroundImageHeight) / 2;

                // Trenutne koordinate slike
                TranslateTransform transform = draggedImage.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggedImage.RenderTransform = transform;
                }

                double newX = transform.X + offsetX;
                double newY = transform.Y + offsetY;

                // Ograničavanje da slika ostane unutar granica velike slike
                double minX = imageLeft;
                double minY = imageTop;
                double maxX = imageLeft + backgroundImageWidth - draggedImage.ActualWidth;
                double maxY = imageTop + backgroundImageHeight - draggedImage.ActualHeight;

                // Ograničavanje X koordinate
                if (newX < minX)
                    newX = minX;
                else if (newX > maxX)
                    newX = maxX;

                // Ograničavanje Y koordinate
                if (newY < minY)
                    newY = minY;
                else if (newY > maxY)
                    newY = maxY;

                transform.X = newX;
                transform.Y = newY;

                startPoint1 = currentPoint;
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
            {
                draggedImage = image;
                startPoint1 = e.GetPosition(Canvas);
                draggedImage.CaptureMouse();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (draggedImage != null)
            {
                // Oslobađamo klik miša kada korisnik pusti sliku
                draggedImage.ReleaseMouseCapture();
                draggedImage = null;
            }
        }

        private void EnableListViewItem(string naziv)
        {
            foreach (var item in ListView1.Items)
            {
                if (item is Kosarkas kosarkas && kosarkas.Slika.Contains(naziv))
                {
                    var listViewItem = (ListViewItem)ListView1.ItemContainerGenerator.ContainerFromItem(item);
                    if (listViewItem != null)
                    {
                        listViewItem.IsEnabled = true;
                    }
                }
            }
        }


        private void Ukloni(object sender, RoutedEventArgs e)
        {
            
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is Image image)
                {
                    Canvas.Children.Remove(image);
                    string[] niz = image.Source.ToString().Split('/');
                    string naziv ="/"+niz[niz.Length-2]+"/"+niz[niz.Length-1];
                    EnableListViewItem(naziv);
                    
                    
                }
            }
                   
        }

        private void RemoveListViewItem(string naziv)
        {
            var itemToRemove = kosarkasi.FirstOrDefault(k => k.Slika.Contains(naziv));
            
            List<Kosarkas> kosarkasi2=new List<Kosarkas>();
            foreach(Kosarkas k in kosarkasi)
            {
                if(k!=itemToRemove)
                    kosarkasi2.Add(k);
            }
            ViewDataGrid.ItemsSource = kosarkasi2;
            ListView1.ItemsSource = kosarkasi2;
        }

        private void Izbrisi(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is Image image)
                {
                    Canvas.Children.Remove(image);
                    string[] niz = image.Source.ToString().Split('/');
                    string naziv = "/" + niz[niz.Length - 2] + "/" + niz[niz.Length - 1];
                    RemoveListViewItem(naziv);

                    List<Kosarkas> kosarkasi2 = new List<Kosarkas>();
                    //MessageBox.Show(niz[niz.Length - 1]);

                    foreach (Kosarkas k in kosarkasi)
                    {
                        if (k.Slika.Contains(niz[niz.Length - 1]))
                        {

                        }
                        else kosarkasi2.Add(k);
                    }

                    StreamWriter sw = null;
                    try
                    {
                        sw = new StreamWriter("Kosarkasi.txt");
                        foreach (Kosarkas k in kosarkasi2)
                        {
                            sw.WriteLine(k.toString());
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
                        }
                    }
                    kosarkasi = kosarkasi2;
                    osvezi();
                }
            }

        }

        private void IzmeniIgraca(object sender, RoutedEventArgs e)
        {
            Image i = sender as Image;
            string[] ime = i.Source.ToString().Split('/');
            string naziv = ime[ime.Length-1];
            Izmena d = new Izmena(this,naziv);
            d.Closed += SecondWindow_Closed;
            d.Closing += SecondWindow_Closed;
            d.Show();
            osvezi();
            refresh();

        }

        private void DodajIgraca(object sender, RoutedEventArgs e)
        {
            Dodavanje d=new Dodavanje(this);
            d.Closed +=SecondWindow_Closed;
            d.Closing += SecondWindow_Closed;
            d.Show();
            osvezi();
            refresh();

        }
        private void SecondWindow_Closed(object sender, EventArgs e)
        {
            osvezi();
            refresh();
        }

        private void refresh()
        {
            ListView1.ItemsSource = kosarkasi;
            ListView1.Items.Refresh();
            trvKlub.ItemsSource = sve;
        }

        public void osvezi()
        {
            StreamReader sr = null;
            string jmbg;
            string ime;
            string prezime;
            string pozicija;
            string nacionalnost;
            int brojDresa;
            int brojUtakmica;
            int brojPoena;
            string slika;
            string linija;

            try
            {
                sr = new StreamReader("kosarkasi.txt");

                kosarkasi.Clear();
                while ((linija = sr.ReadLine()) != null)
                {
                    linija = linija.Trim();
                    string[] lineParts = linija.Split(',');
                    jmbg = lineParts[0];
                    ime = lineParts[1];
                    prezime = lineParts[2];
                    pozicija = lineParts[3];
                    nacionalnost = lineParts[4];
                    brojDresa = Int32.Parse(lineParts[5]);
                    brojUtakmica = Int32.Parse(lineParts[6]);
                    brojPoena = Int32.Parse(lineParts[7]);
                    slika = lineParts[8];

                    Kosarkas k = new Kosarkas(jmbg, ime, prezime, pozicija, nacionalnost, brojDresa, brojUtakmica, brojPoena, slika);
                    kosarkasi.Add(k);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            int id;
            string naziv;
            string grad;
            string logo;

            try
            {
                sr = new StreamReader("klubovi.txt");

                klubovi.Clear();
                while ((linija = sr.ReadLine()) != null)
                {
                    
                    linija = linija.Trim();
                    string[] lineParts = linija.Split(',');
                    id = Int32.Parse(lineParts[0]);
                    naziv = lineParts[1];
                    grad = lineParts[2];
                    logo = lineParts[3];
                    


                    Klub k = new Klub(id, naziv, grad, logo);
                    klubovi.Add(k);
                    //MessageBox.Show(logo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
            sve.Clear();
            Svi.SviKlubovi=klubovi;
            sve.Add(Svi);
            trvKlub.ItemsSource = sve;
            

            ViewDataGrid.ItemsSource = kosarkasi;
            ListView1.ItemsSource = kosarkasi;
        }

        

        private void DodajKlub(object sender, MouseButtonEventArgs e)
        {
            DodavanjeKluba d = new DodavanjeKluba(this);
            d.Closed += SecondWindow_Closed;
            d.Closing += SecondWindow_Closed;
            d.Show();
            osvezi();
            refresh();

        }


        Point startPoint4 = new Point();

        private void Image_MouseDown2(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image image)
            {
                draggedImage2 = image;
                startPoint3 = e.GetPosition(Canvas2);
                draggedImage2.CaptureMouse();
            }
        }

        private void Image_MouseUp2(object sender, MouseButtonEventArgs e)
        {
            if (draggedImage2 != null)
            {
                // Oslobađamo klik miša kada korisnik pusti sliku
                draggedImage2.ReleaseMouseCapture();
                draggedImage2 = null;
            }
        }
        private Image draggedImage2;
        private Point startPoint3;

        private void Image_MouseMove2(object sender, MouseEventArgs e)
        {
            if (Evropa != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition(Evropa);  // Pozicija miša u odnosu na veliku sliku

                double offsetX = currentPoint.X - startPoint3.X;
                double offsetY = currentPoint.Y - startPoint3.Y;

                // Dimenzije velike slike (granice)
                double backgroundImageWidth = Evropa.ActualWidth;
                double backgroundImageHeight = Evropa.ActualHeight;

                // Dimenzije grid-a
                double gridWidth = Canvas2.ActualWidth;
                double gridHeight = Canvas2.ActualHeight;

                // Pocetne koordinate velike slike u gridu
                double imageLeft = (gridWidth - backgroundImageWidth) / 2;
                double imageTop = (gridHeight - backgroundImageHeight) / 2;

                // Trenutne koordinate slike
                TranslateTransform transform = draggedImage2.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    draggedImage2.RenderTransform = transform;
                }

                double newX = transform.X + offsetX;
                double newY = transform.Y + offsetY;

                // Ograničavanje da slika ostane unutar granica velike slike
                double minX = imageLeft;
                double minY = imageTop;
                double maxX = imageLeft + backgroundImageWidth - draggedImage2.ActualWidth;
                double maxY = imageTop + backgroundImageHeight - draggedImage2.ActualHeight;

                // Ograničavanje X koordinate
                if (newX < minX)
                    newX = minX;
                else if (newX > maxX)
                    newX = maxX;

                // Ograničavanje Y koordinate
                if (newY < minY)
                    newY = minY;
                else if (newY > maxY)
                    newY = maxY;

                transform.X = newX;
                transform.Y = newY;

                startPoint3 = currentPoint;
            }
        }


        private void Image_DragEnter2(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myImageFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Image_Drop2(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myImageFormat"))
            {

                var imageSource = e.Data.GetData("myImageFormat") as ImageSource;
                if (imageSource != null)
                {
                    MenuItem mi1 = new MenuItem();
                    mi1.Header = "Izbrisi klub";
                    mi1.Click += Izbrisi2;
                    MenuItem mi2 = new MenuItem();
                    mi2.Header = "Ukloni sliku";
                    mi2.Click += Ukloni2;
                    ContextMenu cm = new ContextMenu();
                    cm.Items.Add(mi1);
                    cm.Items.Add(mi2);
                    Image newImage = new Image
                    {
                        Source = imageSource,
                        Width = 110,
                        Height = 65,
                        ContextMenu = cm
                    };

                    Point dropPosition = e.GetPosition(Canvas2);
                    newImage.HorizontalAlignment = HorizontalAlignment.Left;
                    newImage.VerticalAlignment = VerticalAlignment.Top;

                    // Dodajemo TranslateTransform kako bismo pratili pomeranje slike
                    TranslateTransform transform = new TranslateTransform();
                    newImage.RenderTransform = transform;

                    // Postavljamo početnu poziciju slike
                    transform.X = dropPosition.X;
                    transform.Y = dropPosition.Y;

                    // Dodajemo sliku u grid i dodeljujemo event handlere za pomeranje

                    newImage.MouseLeftButtonDown += Image_MouseDown2;
                    newImage.MouseMove += Image_MouseMove2;
                    newImage.MouseLeftButtonUp += Image_MouseUp2;
                    newImage.MouseDown += Image_MouseDown2;
                    newImage.MouseUp += Image_MouseUp2;
                    Canvas2.Children.Add(newImage);


                }

            }
        }

        private void Ukloni2(object sender, RoutedEventArgs e)
        {

            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is Image image)
                {
                    Canvas2.Children.Remove(image);
                    string[] niz = image.Source.ToString().Split('/');
                    string naziv = "/" + niz[niz.Length - 2] + "/" + niz[niz.Length - 1];
                    EnableTreeViewItem(naziv);
                }
            }

        }

        private void RemoveTreeViewItem(string naziv)
        {
            var itemToRemove = klubovi.FirstOrDefault(k => k.Logo.Contains(naziv));

            ObservableCollection<Klub> klubovi2 = new ObservableCollection<Klub>();
            foreach (Klub k in klubovi)
            {
                if (k != itemToRemove)
                    klubovi2.Add(k);
            }
            sve.Clear();
            Svi.SviKlubovi=klubovi2;
            sve.Add(Svi);
            trvKlub.ItemsSource = sve;
            
            
        }

        private void Izbrisi2(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Parent is ContextMenu contextMenu)
            {
                if (contextMenu.PlacementTarget is Image image)
                {
                    Canvas2.Children.Remove(image);
                    string[] niz = image.Source.ToString().Split('/');
                    string naziv = "/" + niz[niz.Length - 2] + "/" + niz[niz.Length - 1];
                    RemoveTreeViewItem(naziv);

                    ObservableCollection<Klub> klubovi2 = new ObservableCollection<Klub>();
                    //MessageBox.Show(niz[niz.Length - 1]);

                    foreach (Klub k in klubovi)
                    {
                        if (k.Logo.Contains(niz[niz.Length - 1]))
                        {

                        }
                        else klubovi2.Add(k);
                    }

                    StreamWriter sw = null;
                    try
                    {
                        sw = new StreamWriter("klubovi.txt");
                        foreach (Klub k in klubovi2)
                        {
                            sw.WriteLine(k.toString());
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
                        }
                    }
                    klubovi = klubovi2;
                    osvezi();
                }
            }

        }

        private void EnableTreeViewItem(string naziv)
        {
            foreach (var item in trvKlub.Items)
            {
                if (item is Klub kl && kl.Logo.Contains(naziv))
                {
                    var treeViewItem = (TreeViewItem)trvKlub.ItemContainerGenerator.ContainerFromItem(item);
                    if (treeViewItem != null)
                    {
                        treeViewItem.IsEnabled = true;
                    }
                }
            }
        }

        private void ListView_PreviewMouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            startPoint4 = e.GetPosition(null);
        }

        private void ListView_MouseMove2(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint4 - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                Image image = e.OriginalSource as Image;

                if (image != null)
                {
                    TreeViewItem treeVievItem = FindAncestor<TreeViewItem>(image);

                    if (treeVievItem == null) return;

                    // Dodato: Proveri da li je ListViewItem već onemogućen
                    if (!treeVievItem.IsEnabled)
                    {
                        return;
                    }
                    /*
                    Klub k = (Klub)trvItems.ItemContainerGenerator.ItemFromContainer(treeVievItem);

                    */
                    

                    DataObject dragData = new DataObject("myImageFormat", image.Source);

                    DragDrop.DoDragDrop(image, dragData, DragDropEffects.Move);

                    // Dodato: Onemogući ListViewItem nakon prevlačenja
                    treeVievItem.IsEnabled = false;
                }
            }
        }

        private void IzmeniKlub(object sender, RoutedEventArgs e)
        {
            Image i = sender as Image;
            string[] ime = i.Source.ToString().Split('/');
            string naziv = ime[ime.Length - 1];
            IzmenaKluba d = new IzmenaKluba(this, naziv);
            d.Closed += SecondWindow_Closed;
            d.Closing += SecondWindow_Closed;
            d.Show();
            osvezi();
            refresh();

        }
    }
}
