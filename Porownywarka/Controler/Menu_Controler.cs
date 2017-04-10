using Porownywarka.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Porownywarka
{
    public partial class Menu : Window
    {

        public Menu()
        {
            
            InitializeComponent();
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            Login LoginWindow = new Login();
            this.Close();
            LoginWindow.Show();
        }
        private void button_Credits_Click(object sender, RoutedEventArgs e)
        {
            Creditsxaml credits = new Creditsxaml();
            this.Close();
            credits.Show();
        }
        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
