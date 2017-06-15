using Porownywarka.pl.allegro.webapi;
using Porownywarka.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Porownywarka.Controler;

namespace Porownywarka
{
    public partial class Login : Window
    {
        
        string User;
        string Password;
        const string webapiKey = "xxxxxx";
        long versionKey  = 1491826292;
        public static LINQToSQLClassDataContext dc = new LINQToSQLClassDataContext(DatabaseConnection.Connection);

        //s515d776
        serviceService service;
        string sessionHandle;   // uchwyt sesji jako zmienna globalna
    public Login()
        {
            InitializeComponent();
            service = new serviceService();
      
        }

        public void Zaloguj()
        {
       
            User = Username_TextBok.Text;
            Password = Password_PasswordBox.Password;
        //    Password= "515d776a98B21be1";
            service.doQuerySysStatus(4, 1, webapiKey, out versionKey);

            long hashoffset = 0; long serverTime = 0;    //deklaracje zmiennych zwracanych przez funkcję doLogin
           
                sessionHandle =
                    service.doLogin(
                   User,   //       loginTxt.Text,      //login został wpisany w polu tekstowym
                 Password,//   pswrdTxt.Text,      //hasło
                    1,                  //parametr "countrycode", 1-Polska
                    webapiKey,          //klucz webapi - zadeklarowany wcześniej jako stała
                    versionKey,         //klucz wersji - jw.
                    out hashoffset,   
                    out serverTime);    // dodatkowe wartości zwracane przez funkcję.   
        }



        private void return_button_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.Show();
        }


        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
       
            try
            {
          //      Zaloguj();
           
                var FirstUser = dc.Customers.First(x => x.Username == Username_TextBok.Text.Trim());
                if (FirstUser == null) return;
                if (FirstUser.Username.Trim() != Username_TextBok.Text ||
                    FirstUser.Password.Trim() != Password_PasswordBox.Password.Trim()) { label.Content = "Bad username or Password";  return; }
           
                var searched = new Searched(FirstUser.IDCustomer);
                this.Close();
                searched.Show();
            }
            catch (Exception error)
            {
                Label_Warnings.Content =(error.Message);
                return;
       
            }
        }
    }
}
