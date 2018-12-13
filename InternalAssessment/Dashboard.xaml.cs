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

namespace InternalAssessment
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            studentinfo studentinfo = new studentinfo();
            studentinfo.Show();
            this.Close();
        }

        private void Subjectbtn_Click(object sender, RoutedEventArgs e)
        {
            subject subject = new subject();
            subject.Show();
            this.Close();
        }

        private void Firstiabtn_Click(object sender, RoutedEventArgs e)
        {
            firstia firstia = new firstia();
            firstia.Show();
            this.Close();
        }

        private void Secondiabtn_Click(object sender, RoutedEventArgs e)
        {
            secondia secondia = new secondia();
            secondia.Show();
            this.Close();
        }

        private void Jabtn_Click(object sender, RoutedEventArgs e)
        {
            jamarks jamarks = new jamarks();
            jamarks.Show();
            this.Close();
        }

        private void Reportbtn_Click(object sender, RoutedEventArgs e)
        {
            report report = new report();
            report.Show();
            this.Close();
        }

        private void Exitbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void search_btn(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.Show();
            this.Close();
        }

        private void delete_btn(object sender, RoutedEventArgs e)
        {
            delete delete = new delete();
            delete.Show();
            this.Close();
            
          
        }
    }
}
