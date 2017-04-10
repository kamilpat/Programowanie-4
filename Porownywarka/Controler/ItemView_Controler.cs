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
    public partial class ItemView : Window
    {
        Label []  parametry_lab;
        public ItemView(ItemsListType item)
        {
       
            InitializeComponent();
            if (item.additionalInfo!=null)
    label3.Content+=item.additionalInfo.ToString();
            if (item.itemId!=null)
            label2.Content+=item.itemId.ToString();
            if (item.itemTitle!=null)
                Title.Content += item.itemTitle;
            if (item.priceInfo!=null)
                label4.Content+=item.priceInfo[0].priceValue.ToString();
            if (item.timeToEnd!=null)
            label.Content+=item.timeToEnd;
            if (item.categoryId!=null)
                CategoryID_lab.Content += item.categoryId.ToString();
            
            
          
            if (item.photosInfo[0].photoUrl!=null)
            {
                var fullFilePath = item.photosInfo[0].photoUrl;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();

                image.Source = bitmap;
            }

            if (item.parametersInfo!=null)
            {
                parametry_lab = new Label[100];
                int i = 0;

                foreach (var parameter in item.parametersInfo)
                {
                    parametry_lab[i] = new Label();
                    parametry_lab[i].Content = parameter.parameterName;
                    parametry_lab[i].Margin = new Thickness(20, 30 * i + 100, 0, 0);

                    i++;
                    
                }
            }

         
     
        }
        private void return_button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            this.Close();
            menu.Show();
        }
    }
}
