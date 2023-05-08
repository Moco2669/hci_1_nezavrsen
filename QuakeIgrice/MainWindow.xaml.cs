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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuakeIgrice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataIO serializer=new DataIO();

        public static BindingList<Korisnik> nalozi { get; set; }

        public MainWindow()
        {
            nalozi = serializer.DeSerializeObject<BindingList<Korisnik>>("nalozi.xml");
            if (nalozi == null)
            {
                nalozi = new BindingList<Korisnik>();
            }

            InitializeComponent();
        }

        public MainWindow(string message)
        {
            InitializeComponent();
            TbIme.Text = message;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            foreach(Korisnik k in nalozi)
            {
                if (TbIme.Text == k.ime)
                {
                    if(TbSifra.Password == k.sifra)
                    {
                        Prikaz prikaz = new Prikaz(k);
                        prikaz.Show();
                        this.Close();
                    }
                    else
                    {
                        //TODO Pogresna sifra
                    }
                }
            }
        }
    }
}
