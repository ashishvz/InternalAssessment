using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for jamarks.xaml
    /// </summary>
    public partial class jamarks : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dataAdapter;
        public jamarks()
        {
            InitializeComponent();
            cbcourse.Items.Add("BCA");
            cbcourse.Items.Add("BCS");
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Program Files (x86)\ashdevelopers\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
            connection = new SqlConnection(ConnectionString);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbcourse.Text != "" && subtxt.Text != "" && rollno.Text != "" && jmark.Text != "" && amark.Text != "")
            {
                try
                {

                    cmd = new SqlCommand("update dbo." + cbcourse.Text + subtxt.Text + " set journal='" + jmark.Text + "',atten='" + amark.Text + "' where rollno='" + rollno.Text + "';", connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                    filldata();
                }
            }
            else
            {
                MessageBox.Show("Please enter the data in all fields!");
                
            }
        }

        private void Load_btn_Click(object sender, RoutedEventArgs e)
        {
            if (cbcourse.Text != "" && subtxt.Text != "")
            {
                filldata();
            }
            else
            {
                MessageBox.Show("Select the Course or enter the subject!;");
            }
        }
        public void filldata()
        {
            try
            {

                connection.Open();
                cmd = new SqlCommand("select rollno,fname,lname,firstia,secondia,journal,atten from dbo." + cbcourse.Text + subtxt.Text + ";", connection);
                DataSet ds = new DataSet();
                dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(ds);
                datagridja.ItemsSource = ds.Tables[0].DefaultView;
                connection.Close();


            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
                connection.Close();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }
    }
}