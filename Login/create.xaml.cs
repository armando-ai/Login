using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using DocumentFormat.OpenXml.Spreadsheet;
using Login.DataSet1TableAdapters;
namespace Login
{
    /// <summary>
    /// Interaction logic for create.xaml
    /// </summary>
    public partial class create : Window
    {
        UsersTableAdapter ad = new UsersTableAdapter();
       
        
        public create()
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
           //add validtion for blanks 

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow row = dt.Rows[i];

                    if (row["username"].ToString().Equals(Username.Text))
                    {
                        check = false;
                        MessageBox.Show("User with that username already exists");
                        break;
                    }
                    
                    if (row["email"].ToString().Equals(Email.Text))
                    {
                        check = false;
                        MessageBox.Show("User with that email already exists");
                        break;
                    }
                }



            }
            if(check == true)
            {
                ad.Insert(Name.Text, Username.Text, Password.Text, Email.Text);
                MessageBox.Show("User "+ Username.Text+" has been created");
                this.Hide();
                new MainWindow().Show();
            }

            //will do direct insert


           
        }

        private void btnDoIt_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new MainWindow().Show();
        }
    }
}
