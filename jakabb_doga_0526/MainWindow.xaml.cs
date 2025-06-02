using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace jakabb_doga_0526
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MySqlConnection kapcsolat = new MySqlConnection("server = localhost;database = jakab_11a; uid = 'root'; password = ''");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kapcsolat.Open();
            var lek = new MySqlCommand("SELECT * FROM filmek", kapcsolat).ExecuteReader();
            lbadatok.Items.Clear(); 
            while (lek.Read())
            {
                lbadatok.Items.Add(lek["filmazon"] + ";" + lek["cim"] + ";" + lek["ev"] + ";" + lek["szines"] + ";" + lek["mufaj"] + ";" + lek["hossz"]);
            }
            kapcsolat.Close();
        }

        private void lbadatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbadatok.SelectedItem == null)
                return;

            var valasztott = lbadatok.SelectedItem.ToString();
            var adatok = valasztott.Split(';');
            lbfilmazon.Content = adatok[0];
            tb1.Text = adatok[1];
            tb2.Text = adatok[2];
            tb3.Text = adatok[3];
            tb4.Text = adatok[4];
            tb5.Text = adatok[5];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            kapcsolat.Open();
            var upd = new MySqlCommand($"update filmek set cim = '{tb1.Text}',ev = {tb2.Text},szines = '{tb3.Text}',mufaj = '{tb4.Text}', hossz = {tb5.Text} where filmazon = '{lbfilmazon.Content}'", kapcsolat).ExecuteNonQuery();
            kapcsolat.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            kapcsolat.Open();
            var del = new MySqlCommand($"delete from filmek where filmazon = '{lbfilmazon.Content}'", kapcsolat).ExecuteNonQuery();
            kapcsolat.Close();
            lbadatok.Items.Remove(lbadatok.SelectedItem);
        }
    }
}
