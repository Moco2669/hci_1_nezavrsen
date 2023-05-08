using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace QuakeIgrice
{
    /// <summary>
    /// Interaction logic for Prikaz.xaml
    /// </summary>
    public partial class Prikaz : Window
    {
        private DataIO serializer = new DataIO();

        public static BindingList<Stranica> stranice { get; set; }

        private static Korisnik korisnik;

        public Prikaz(Korisnik k)
        {
            korisnik = k;
            stranice = serializer.DeSerializeObject<BindingList<Stranica>>("stranice.xml");
            if (stranice == null)
            {
                stranice = new BindingList<Stranica>();
            }
            DataContext = this;
            InitializeComponent();
            if (k.tip == Tip.K)
            {
                btnDodaj.Visibility = Visibility.Collapsed;
                btnObrisi.Visibility = Visibility.Collapsed;
                Cekbox.Visibility = Visibility.Collapsed;
            }
            tbLogovaniKorisnik.Text = k.ime;
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Editor editor= new Editor(stranice);
            editor.ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            serializer.SerializeObject<BindingList<Stranica>>(stranice, "stranice.xml");
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            BindingList<Stranica> zabrisanje = new BindingList<Stranica>();
            foreach(Stranica s in stranice)
            {
                if (s.cekiran)
                {
                    zabrisanje.Add(s);
                }
            }
            foreach(Stranica s in zabrisanje)
            {
                stranice.Remove(s);
            }
        }

        private void BtnOdjava_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void BtnIzlaz_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void hlClick(object sender, RoutedEventArgs e)
        {
            Stranica s = DGridIgrice.SelectedItem as Stranica;
            if (korisnik.tip == Tip.A)
            {
                Editor editor = new Editor(stranice, s);
                editor.ShowDialog();
            }
            else
            {
                FensiPrikaz fp = new FensiPrikaz(s);
                fp.ShowDialog();
            }
        }
    }
}
