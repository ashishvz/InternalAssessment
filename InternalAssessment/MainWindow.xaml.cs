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
using System.Data.SqlClient;

namespace InternalAssessment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
            
            ConnectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Program Files (x86)\ashdevelopers\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
            connection = new SqlConnection(ConnectionString);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmd.CommandText = "Select * from dbo.login where username='" + usertxt.Text.ToString() + "'and pass='" + passbox.Password.ToString() + "';";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    i++;
                }

                if (i == 1)
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    errorlab.Content = "Incorrect Username or password;)";
                    errorlab.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                connection.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            usertxt.Text = "";
            passbox.Password = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void change_credentials(object sender, RoutedEventArgs e)
        {
            ChangeCredentials change = new ChangeCredentials();
            change.Show();
            this.Close();
        }

        private void Usertxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Contactbtn_Click(object sender, RoutedEventArgs e)
        {
            ContactUs contact = new ContactUs();
            contact.Show();
            this.Close();
        }
    }
}
