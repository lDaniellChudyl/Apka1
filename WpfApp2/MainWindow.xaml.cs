using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace WpfApp2
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    using WpfApp2;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var converter = new BrushConverter();
            
            
            var db = new MembersDBContext();
            ICollectionView itemsView = CollectionViewSource.GetDefaultView(db.Dane.Where(r => r.Number == r.Number).ToList());
            Staty.Text = $"{db.Dane.Count()} Użytkowników";
            membersDataGrid.ItemsSource = itemsView;




        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        private void membersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dodawanie d = new Dodawanie();
            d.M = this;
            d.Show();
        }

        private void usun(object sender, RoutedEventArgs e)
        {
            Button butt = (Button)sender;
            var Number = butt.Tag.ToString();

            var db = new MembersDBContext();
            var selected = db.Dane.Where(r => r.Number == int.Parse(Number)).FirstOrDefault();
            db.Remove(selected);
            db.SaveChanges();
            ICollectionView itemsView = CollectionViewSource.GetDefaultView(db.Dane.Where(r => r.Number == r.Number).ToList());
            Staty.Text = $"{db.Dane.Count()} Użytkowników";
            membersDataGrid.ItemsSource = itemsView;
        }

        private void edit(object sender, RoutedEventArgs e)
        {
            membersDataGrid.Visibility = Visibility.Collapsed;
            edit_content.Visibility = Visibility.Visible;

            Button butt = (Button)sender;
            var Number = butt.Tag.ToString();

            edit_button.Tag = Number;

            var db = new MembersDBContext();
            var result = db.Dane.SingleOrDefault(b => b.Number == int.Parse(Number));

            if (result != null)
            {
                name.Text = result.Name.ToString();
                email.Text = result.Email;
                phone.Text = result.Phone;
                position.Text = result.Position;
            }
        }

        private void edit_save(object sender, RoutedEventArgs e)
        {
            Button butt = (Button)sender;
            var Number = butt.Tag.ToString();

            var db = new MembersDBContext();
            var result = db.Dane.SingleOrDefault(b => b.Number == int.Parse(Number));

            if (result != null)
            {
                result.Name = name.Text;
                result.Character = name.Text[0].ToString();
                result.Email = email.Text;
                result.Phone = phone.Text;
                result.Position = position.Text;
                db.SaveChanges();

                ICollectionView itemsView = CollectionViewSource.GetDefaultView(db.Dane.Where(r => r.Number == r.Number).ToList());
                membersDataGrid.ItemsSource = itemsView;
            }

            edit_content.Visibility = Visibility.Collapsed;
            membersDataGrid.Visibility = Visibility.Visible;

            edit_button.Tag = null;
        }

        private void edit_cancel(object sender, RoutedEventArgs e)
        {
            edit_content.Visibility = Visibility.Collapsed;
            membersDataGrid.Visibility = Visibility.Visible;

            edit_button.Tag = null;
        }

        private void SearchUsers(object sender, TextChangedEventArgs e)
        {
            TextBox searchtxt = (TextBox)sender;

            if (searchtxt.Text != string.Empty)
                txtFilterText.Visibility = Visibility.Hidden;
            else
                txtFilterText.Visibility = Visibility.Visible;

            var db = new MembersDBContext();
            ICollectionView obView = CollectionViewSource.GetDefaultView(db.Dane.Where(r => r.Number == r.Number).ToList());

            obView.Filter = delegate (object o)
            {
                var item = (Member)o;

                return item?.Name.IndexOf(searchtxt.Text, StringComparison.OrdinalIgnoreCase) != -1 || searchtxt.Text == string.Empty;
            };

            membersDataGrid.ItemsSource = obView;

        }
    }
    
}
