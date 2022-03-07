using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections;

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
using Login.DataSet1TableAdapters;
namespace Login
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
       
        public login()
        {
            InitializeComponent();
        }
        private void btnDoIt_Click(object sender, RoutedEventArgs e)
        {
            bool check = true;
            DataTable dt;
            Hashtable ht = new Hashtable();
            String sql = "Select * from Users";
            dt = ExDB.GetDataTable("AwesomeDB", ht, sql);
            int x = 0;

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];
                    //add validation fro blanks
                    if (row["username"].ToString().Equals(Username.Text) && row["password"].ToString().Equals(Password.Text))
                    {
                        check = false;
                        x = i;
                        break;
                    }

                   
                }



            }
            DataRow row1 = dt.Rows[x];
            if (check == false)
            {

                MessageBox.Show("Success!");
                this.Hide();
                new LoggedIn(row1).Show();
            }
            else {
                MessageBox.Show("Credentials are not valid");
            }

        }

        private void btnDoIt_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new MainWindow().Show();
        }
    }
}
