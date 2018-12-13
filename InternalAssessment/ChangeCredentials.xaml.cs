using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace InternalAssessment
{
    /// <summary>
    /// Interaction logic for ChangeCredentials.xaml
    /// </summary>
    public partial class ChangeCredentials : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlCommand cmd1 = new SqlCommand();
        private SqlDataReader reader;
        public ChangeCredentials()
        {
            InitializeComponent();
            newun.IsEnabled = false;
            newpw.IsEnabled = false;
            change_Copy.IsEnabled = false;
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ashis\source\repos\InternalAssessment\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
            connection = new SqlConnection(ConnectionString);
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmd = new SqlCommand("Select username from dbo.login where username='" + oldun.Text + "'and pass='" + oldpw.Password.ToString() + "';", connection);
                connection.Open();
                reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                }
                if (i == 1)
                {
                    newun.IsEnabled = true;
                    newpw.IsEnabled = true;
                    change_Copy.IsEnabled = true;
                    MessageBox.Show("Enter the new Credentials!!;");
                    
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

        private void Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                cmd = new SqlCommand("update dbo.login set pass='" + newpw.Password.ToString() + "' where username='" + oldun.Text + "';",connection);
                cmd1 = new SqlCommand("update dbo.login set username='" + newun.Text + "' where pass='" + newpw.Password.ToString() + "';",connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                MessageBox.Show("UserName and Password Changed;)");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
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

        private void back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
