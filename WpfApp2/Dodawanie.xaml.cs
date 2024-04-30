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

namespace WpfApp2
{
    /// <summary>
    /// Logika interakcji dla klasy Dodawanie.xaml
    /// </summary>
    public partial class Dodawanie : Window
    {

        public MainWindow M {  get; set; }




        public Dodawanie()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Dodawanie1_Click(object sender, RoutedEventArgs e)
        {
            var Db = new MembersDBContext();
            Db.Add<Member>(new Member()
            {
                Character = Inicjaly.Text,
                Email = Email.Text,
                Name = Imie.Text,
                Phone = Telefon.Text,
                Position = Pozycje.Text
            });

            Db.SaveChanges();
            
            var db = new MembersDBContext();
            ICollectionView itemsView = CollectionViewSource.GetDefaultView(db.Dane.Where(r => r.Number == r.Number).ToList());
            M.Staty.Text = $"{db.Dane.Count()} Użytkowników";
            M.membersDataGrid.ItemsSource = itemsView;
        }
    }
}
