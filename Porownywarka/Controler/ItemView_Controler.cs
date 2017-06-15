
using Porownywarka.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Porownywarka
{
    public partial class ItemView : Window
    {
        serviceService service;

        Label []  parametry_lab;
        private string sessionHandle;
        ItemsListType Product;
 
        public ItemView(ItemsListType item,string sessionhandl)
        {
            service = new serviceService();
            Product = item;
            sessionHandle = sessionhandl;
            InitializeComponent();
         
            if (Product.photosInfo[0].photoUrl!=null)
            {
                var fullFilePath = Product.photosInfo[0].photoUrl;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();
            }

            if (Product.parametersInfo!=null)
            {
                parametry_lab = new Label[100];
                var i = 0;

                foreach (var parameter in item.parametersInfo)
                {
                    parametry_lab[i] = new Label();
                    parametry_lab[i].Content = parameter.parameterName;
                    parametry_lab[i].Margin = new Thickness(20, 30 * i + 100, 0, 0);

                    i++;  
                }
            }
            DodatkoweDane();
        }

        private void DodatkoweDane()
        {
            long[] arrayItemsNotFound, arrayItemsAdminKilled;
            var tablicaid = new long[1];
            tablicaid[0] = Product.itemId;
            var dane = service.doGetItemsInfo(sessionHandle, tablicaid, 1, true, 1, true, 1, true, 1, true, 1, true, 0, false, 1, true, out arrayItemsNotFound, out arrayItemsAdminKilled);
   
            File.WriteAllText("stronahtml.html", dane[0].itemInfo.itDescription);

            ZaladujHTML();
        }
        private void ZaladujHTML()
        {
            var text = File.ReadAllText("stronahtml.html", Encoding.UTF8);
            webbrowser.NavigateToString(text);
        }

        private void Dodatkowe_Click(object sender, RoutedEventArgs e)
        {
            DodatkoweDane();
        }

        private void return_button_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.Show();
        }
    }
}
