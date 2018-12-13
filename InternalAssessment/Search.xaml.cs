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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        private SqlConnection Sqlconnection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        public string connection_string;
        private SqlDataReader reader;

        public Search()
        {
            
            InitializeComponent();
            connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Program Files (x86)\ashdevelopers\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
            Sqlconnection = new SqlConnection(connection_string);
            cbcourse.Items.Add("BCA");
            cbcourse.Items.Add("BCS");
        }

        private void Serachbtn_Click(object sender, RoutedEventArgs e)
        {
            if (cbcourse.Text != "" && rolltxt.Text != "" && subtxt.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("select * from " + cbcourse.Text + subtxt.Text + " where rollno='" + rolltxt.Text + "';", Sqlconnection);
                    Sqlconnection.Open();
                    reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        i++;
                    }
                    Sqlconnection.Close();
                    if (i == 0)
                    {
                        MessageBox.Show(rolltxt.Text + " Not Found!!;");
                    }
                    else
                    {
                        try
                        {
                            Sqlconnection.Open();
                            DataSet ds = new DataSet();
                            dataAdapter = new SqlDataAdapter(cmd);
                            dataAdapter.Fill(ds);
                            searchdatagrid.ItemsSource = ds.Tables[0].DefaultView;
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            Sqlconnection.Close();
                        }
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                MessageBox.Show("Please enter all the fields to search!");
            }
           
        }

        private void backbtn(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void Cbcourse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
