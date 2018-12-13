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

namespace InternalAssessment
{
    /// <summary>
    /// Interaction logic for studentinfo.xaml
    /// </summary>
    public partial class studentinfo : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dataAdapter;
        int i = 0;
        public studentinfo()
        {
            InitializeComponent();
            coursecb.Items.Add("BCA");
            coursecb.Items.Add("BCS");
            
            
            ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ashis\source\repos\InternalAssessment\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
            connection = new SqlConnection(ConnectionString);
            filldata();


        }       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

               
              
                cmd = new SqlCommand("insert into studentinfo (rollno,fname,lname,course) values(@rollno,@fname,@lname,@course)", connection);
                cmd.Parameters.AddWithValue("@rollno", rolltxt.Text);
                cmd.Parameters.AddWithValue("@fname", fnametxt.Text);
                cmd.Parameters.AddWithValue("@lname", lnametxt.Text);
                cmd.Parameters.AddWithValue("@course", coursecb.Text);
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
                filldata();
            }
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        public void filldata()
        {
            try
            {

                connection.Open();
                cmd = new SqlCommand("select * from dbo.studentinfo;", connection);
                DataSet ds = new DataSet();
                dataAdapter = new SqlDataAdapter(cmd);

                dataAdapter.Fill(ds);
                datagrid.ItemsSource = ds.Tables[0].DefaultView;
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
