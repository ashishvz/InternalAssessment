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

namespace InternalAssessment
{
    /// <summary>
    /// Interaction logic for delete.xaml
    /// </summary>
    public partial class delete : Window
    {
        private SqlConnection Sqlconnection;
        private SqlCommand cmd = new SqlCommand();
        public string connection_string;
        private SqlDataReader dataReader;

        public delete()
        {
            InitializeComponent();
            connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Program Files (x86)\ashdevelopers\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
            Sqlconnection = new SqlConnection(connection_string);
            cbcourse.Items.Add("BCA");
            cbcourse.Items.Add("BCS");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbcourse.Text != "" && subtxt.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("drop table dbo." + cbcourse.Text + subtxt.Text + ";", Sqlconnection);
                    Sqlconnection.Open();
                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Sqlconnection.Close();
                    MessageBox.Show("subject " + cbcourse.Text + subtxt.Text + " deleted ");
                }
            }
            else
            {
                MessageBox.Show("Please select the course or enter the subject!");
            }
            }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (fromtxt.Text != "" && totxt.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("select rollno from dbo.studentinfo where rollno between '" + fromtxt.Text + "'and'" + totxt.Text + "';", Sqlconnection);
                    Sqlconnection.Open();
                    dataReader = cmd.ExecuteReader();
                    int i = 0;
                    while(dataReader.Read())
                    {
                        i++;
                    }
                    Sqlconnection.Close();
                    if (i != 0)
                    {
                        cmd = new SqlCommand("delete from dbo.studentinfo where rollno between '" + fromtxt.Text + "' and '" + totxt.Text + "';", Sqlconnection);
                        Sqlconnection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deleted RollNo from " + fromtxt.Text + " to " + totxt.Text);
                    }
                    else
                    {
                        MessageBox.Show("Data Doesn't Exists");
                    }
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
            else
            {
                MessageBox.Show("Please enter the rollnos!");
            }

        }

        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {

            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }  
    }
    }

