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

namespace QuakeIgrice
{
    /// <summary>
    /// Interaction logic for FensiPrikaz.xaml
    /// </summary>
    public partial class FensiPrikaz : Window
    {
        public FensiPrikaz(Stranica stranica)
        {
            InitializeComponent();
            tblNaziv.Text = stranica.naslov;
            tblGodina.Text = stranica.godinaIzdavanja.ToString();
            string glavnaPutanja = Directory.GetCurrentDirectory() + "/Podaci/";
            ucitajRtf(glavnaPutanja + stranica.rtfPutanja);
            imgSlika.Source = new BitmapImage(new Uri(stranica.slikaPutanja, UriKind.RelativeOrAbsolute));
        }

        private void ucitajRtf(string putanja)
        {
            TextRange tr;
            FileStream fs;
            if (File.Exists(putanja))
            {
                tr = new TextRange(rtbGlavniTekst.Document.ContentStart, rtbGlavniTekst.Document.ContentEnd);
                fs = new FileStream(putanja, FileMode.OpenOrCreate);
                tr.Load(fs, DataFormats.Rtf);
                fs.Close();
            }
            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
