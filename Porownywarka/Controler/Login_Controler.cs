using Porownywarka.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Porownywarka
{
    public partial class Login : Window
    {

        string User;
        string Password;
        const string webapiKey = "";
        long versionKey  = 1491826292;
        //s515d776
        serviceService service;
        string sessionHandle;   // uchwyt sesji jako zmienna globalna
        LINQToSQLClassDataContext dc = new LINQToSQLClassDataContext(Properties.Settings.Default.GraphicCardConnectionString);
        public Login()
        {
            InitializeComponent();
            service = new serviceService();
            //         if (dc.DatabaseExists())
            //         {
            //         dataGrid.ItemsSource = dc.Cards.Where(x => x.Category.Contains(textBox.Text));
            //     dc.Users.Where(x => x.UserName == Username_TextBok.Text && x.Password == Password_TextxBox.Text).Take(1);
            //         }
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



        


        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
       
            try
            {
                Zaloguj();
                MainWindow mainWindow = new MainWindow();
               this.Close();
               mainWindow.Show();
            }
            catch (Exception error)
            {
                Label_Warnings.Content =(error.Message);
                return;
       
            }
        }
    }
}
