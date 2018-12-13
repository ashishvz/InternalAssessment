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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace InternalAssessment
{
    /// <summary>
    /// Interaction logic for firstia.xaml
    /// </summary>
    public partial class firstia : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dataAdapter;
        public firstia()
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
                double fia = Convert.ToDouble(marks.Text);
                fia = fia / 5;
                cmd = new SqlCommand("update dbo."+cbcourse.Text+subtxt.Text+" set firstia='" + fia + "' where rollno='" + rollno.Text + "';", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                studentinfo studentinfo = new studentinfo();
                filldata();
            }
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void Load_btn_Click(object sender, RoutedEventArgs e)
        {
            filldata();
        }

        private void export_xl(object sender, RoutedEventArgs e)
        {
            try
            {

                connection.Open();
                cmd = new SqlCommand("select rollno,fname,lname,firstia from dbo."+cbcourse.Text+subtxt.Text+";", connection);
                dataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("dbo."+cbcourse.Text+subtxt.Text);
                dataAdapter.Fill(dt);

               




                 dt.WriteXml("D:\\"+cbcourse.Text+subtxt.Text+".xls");
                 MessageBox.Show("Exported succefully D:\\" + cbcourse.Text + subtxt.Text + ".xls");
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void filldata()
        {
            try
            {

                connection.Open();
                cmd = new SqlCommand("select rollno,fname,lname,firstia from dbo." + cbcourse.Text + subtxt.Text + ";", connection);
                DataSet ds = new DataSet();
                dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(ds);
                datagridfia.ItemsSource = ds.Tables[0].DefaultView;
                connection.Close();


            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
                connection.Close();

            }

        }
    }
}
