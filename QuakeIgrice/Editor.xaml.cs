using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        private int redniBrojItema;
        private string slikaPutanja { get; set; }
        private static BindingList<Stranica> stranice;
        private static Stranica stranica;
        public Editor(BindingList<Stranica> stranice)
        {
            Editor.stranice = stranice;
            Editor.stranica = null;
            slikaPutanja = "";
            redniBrojItema = stranice.Count;
            this.DataContext = this;
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            for (int i = 1; i < 128; ++i)
            {
                cmbFontSize.Items.Add(i);
            }
            cmbFontSize.SelectedIndex = 11;
            cmbFontFamily.SelectedIndex = 3;
            List<Color> boje = new List<Color>();
            boje.Add(Colors.Black);
            boje.Add(Colors.White);
            boje.Add(Colors.Blue);
            boje.Add(Colors.Green);
            boje.Add(Colors.Red);
            boje.Add(Colors.SkyBlue);
            cmbFontColor.ItemsSource = boje;
            cmbFontColor.SelectedIndex = 0;
        }

        public Editor(BindingList<Stranica> stranice, Stranica stranica)
        {
            Editor.stranice = stranice;
            slikaPutanja = stranica.slikaPutanja;
            redniBrojItema = 0;
            Editor.stranica = stranica;
            foreach(Stranica s in stranice)
            {
                if (s == stranica)
                {
                    break;
                }
                redniBrojItema++;
            }
            this.DataContext = this;
            InitializeComponent();
            string glavnaPutanja = Directory.GetCurrentDirectory() + "/Podaci/";
            ucitajTekst(glavnaPutanja + stranica.rtfPutanja);
            tbNaziv.Text = stranica.naslov;
            tbGodina.Text = stranica.godinaIzdavanja.ToString();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            for(int i = 1; i < 128; ++i)
            {
                cmbFontSize.Items.Add(i);
            }
            cmbFontSize.SelectedIndex = 11;
            cmbFontFamily.SelectedIndex = 3;
            List<Color> boje = new List<Color>();
            boje.Add(Colors.Black);
            boje.Add(Colors.White);
            boje.Add(Colors.Blue);
            boje.Add(Colors.Green);
            boje.Add(Colors.Red);
            boje.Add(Colors.SkyBlue);
            cmbFontColor.ItemsSource = boje;
            cmbFontColor.SelectedIndex = 0;
        }

        private void BtnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            string naziv = tbNaziv.Text;
            string putanja = slikaPutanja;
            string glavnaPutanja = Directory.GetCurrentDirectory() + "/Podaci/";
            string nazivRtf = naziv;
            sacuvajTekst(glavnaPutanja+nazivRtf);
            if (Editor.stranica == null)
            {
                Stranica s = new Stranica(int.Parse(tbGodina.Text), naziv, putanja, nazivRtf, DateTime.Now);
                Editor.stranica = s;
                stranice.Add(stranica);
            }
            else
            {
                foreach(Stranica s in stranice)
                {
                    if (Editor.stranica == s)
                    {
                        stranice.Remove(s);
                        stranice.Add(Editor.stranica);
                        break;
                    }
                }
            }
            
        }
        private void ucitajTekst(string rtf)
        {
            TextRange tr;
            FileStream fs;
            if (File.Exists(rtf))
            {
                tr = new TextRange(rtbGlavniTekst.Document.ContentStart, rtbGlavniTekst.Document.ContentEnd);
                fs = new FileStream(rtf, FileMode.OpenOrCreate);
                tr.Load(fs, DataFormats.Rtf);
                fs.Close();
            }
            
        }
        private void sacuvajTekst(string rtf)
        {
            TextRange tr;
            FileStream fs;
            tr = new TextRange(rtbGlavniTekst.Document.ContentStart, rtbGlavniTekst.Document.ContentEnd);
            fs = new FileStream(rtf, FileMode.Create);
            tr.Save(fs, DataFormats.Rtf);
            fs.Close();
        }

        private void BtnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFontFamily.SelectedItem!=null && !rtbGlavniTekst.Selection.IsEmpty)
            {
                rtbGlavniTekst.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void CmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFontSize.SelectedItem!=null && !rtbGlavniTekst.Selection.IsEmpty)
            {
                rtbGlavniTekst.Selection.ApplyPropertyValue(Inline.FontSizeProperty, Convert.ToDouble(cmbFontSize.SelectedItem));
            }
        }

        private void BtnSlika_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Слике (*.png;*.jpeg;*.bmp;*.jpg;)|*.png;*.jpeg;*.bmp;*.jpg;";
            if (fileDialog.ShowDialog() == true)
            {
                slikaPutanja = fileDialog.FileName;
            }
            imgSlika.Source = new BitmapImage(new Uri(slikaPutanja, UriKind.RelativeOrAbsolute));
        }

        private void BtnPreview_Click(object sender, RoutedEventArgs e)
        {
            FensiPrikaz fp = new FensiPrikaz(Editor.stranica);
            fp.ShowDialog();
        }

        private void CmbFontColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null && !rtbGlavniTekst.Selection.IsEmpty)
            {
                SolidColorBrush scb = new SolidColorBrush((Color)(cmbFontColor.SelectedItem));
                rtbGlavniTekst.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, scb);
            }
        }
    }
}
