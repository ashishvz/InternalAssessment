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
    /// Interaction logic for report.xaml
    /// </summary>
    public partial class report : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dataAdapter;
        public report()
        {
            InitializeComponent();
            cbcourse.Items.Add("BCA");
            cbcourse.Items.Add("BCS");
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ashis\source\repos\InternalAssessment\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
            connection = new SqlConnection(ConnectionString);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                connection.Open();
                cmd = new SqlCommand("select * from dbo." + cbcourse.Text + subtxt.Text + ";", connection);
                dataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("dbo." + cbcourse.Text + subtxt.Text);
                dataAdapter.Fill(dt);
                dt.WriteXml("D:\\" + cbcourse.Text + subtxt.Text + ".xls");
                MessageBox.Show("Exported succefully D:\\" + cbcourse.Text + subtxt.Text + ".xls");
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void Load_btn_Click(object sender, RoutedEventArgs e)
        {
            filldata();
        }
        public void filldata()
        {
            try
            {

                connection.Open();
                cmd = new SqlCommand("select * from dbo." + cbcourse.Text + subtxt.Text + ";", connection);
                DataSet ds = new DataSet();
                dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(ds);
                datagridfinal.ItemsSource = ds.Tables[0].DefaultView;
                connection.Close();


            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
                connection.Close();

            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void total_btn(object sender, RoutedEventArgs e)
        {
            try
            {
                
                cmd = new SqlCommand("update dbo." + cbcourse.Text + subtxt.Text + " set total= firstia+secondia+journal+atten;", connection);
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
    }
}
