﻿using Porownywarka.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Porownywarka.Controler;

namespace Porownywarka
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 
    /// 

    public partial class SignIn : Window
    {
        public SignIn()
        {
            InitializeComponent();
        }

        public bool CheckIsCorrect()
        {
            if (Username_TextBok.Text.Trim() == "")
            {
                Error_label.Content = "Nazwa nie może być pusta";
                return false;
            }
            if (DatabaseConnection.dc.Customers.Any(x => x.Username.Trim() == Username_TextBok.Text.Trim()))
            {
                Error_label.Content = "Nazwa użytkownika jest zajęta";
                return false;
            }
            if ((Password_PasswordBox.Password.Trim() != "")) return true;
            Error_label.Content = "Hasło nie może być puste";
            return false;
        }

          private void SignIn_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIsCorrect()) return;
            var user = new Customer
            {
                IDCustomer = DatabaseConnection.dc.Customers.Count() + 1,
                Username = Username_TextBok.Text.Trim(),
                Password = Password_PasswordBox.Password.Trim()
            };
    

            user.IDListOfProducts = user.IDCustomer;
            DatabaseConnection.dc.Customers.InsertOnSubmit(user);
            DatabaseConnection.dc.SubmitChanges(); 
            var menu = new Menu();
            this.Close();
            menu.Show();
        }

        private void return_button_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.Show();
        }
    }
}

