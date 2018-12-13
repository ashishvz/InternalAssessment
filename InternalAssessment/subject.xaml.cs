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
    /// Interaction logic for subject.xaml
    /// </summary>
    public partial class subject : Window
    {
    private SqlConnection SqlConnection;
    private SqlCommand cmd;
    private SqlDataAdapter dataAdapter;
    public string ConnectionString;
    public subject()
    {
        InitializeComponent();
        cbcourse.Items.Add("BCA");
        cbcourse.Items.Add("BCS");
        ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ashis\source\repos\InternalAssessment\InternalAssessment\IADatabase.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection = new SqlConnection(ConnectionString);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
            try
            {
                cmd = new SqlCommand("insert into " + cbcourse.Text + subtxt.Text + " (rollno,fname,lname) select rollno,fname,lname from dbo.studentinfo where rollno between '" + fromtxt.Text + "' and '" + totxt.Text + "';", SqlConnection);
                SqlConnection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("RollNo fetched from" + fromtxt.Text + " to " + totxt.Text);
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SqlConnection.Close();
                filldata();
            }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
    try
        {
            cmd = new SqlCommand("create table dbo." + cbcourse.Text + subtxt.Text + "(rollno varchar(10) primary key not null,fname varchar(15) not null,lname varchar(15) not null, firstia float, secondia float,journal float, atten float, total float);", SqlConnection);
            SqlConnection.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Subject Named " + cbcourse.Text + subtxt.Text + " Created!!");
        }
        catch (SqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
        finally
        {
            SqlConnection.Close();
            filldata();
        }
    }

    public void filldata()
    {
        try
        {

            SqlConnection.Open();
            cmd = new SqlCommand("select * from dbo." + cbcourse.Text + subtxt.Text + ";", SqlConnection);
            DataSet ds = new DataSet();
            dataAdapter = new SqlDataAdapter(cmd);

            dataAdapter.Fill(ds);
            subdatagrid.ItemsSource = ds.Tables[0].DefaultView;
            SqlConnection.Close();


        }
        catch (SqlException ex)
        {

            MessageBox.Show(ex.Message);
            SqlConnection.Close();

        }
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        Dashboard dashboard = new Dashboard();
        dashboard.Show();
        this.Close();
    }

        public void load_btn(object sender, RoutedEventArgs e)
        {
            filldata();
        }
    }
}

