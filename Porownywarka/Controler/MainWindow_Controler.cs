using Porownywarka.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Porownywarka
{
    public partial class MainWindow : Window
    {
        public ItemsListType[] itemsList2;
        public ItemsListType SelectedItem;
        public string sessionHandle;
        ////    LINQToSQLClassDataContext dc = new LINQToSQLClassDataContext(Properties.Settings.Default.GraphicCardConnectionString);
        const string webapiKey = "abfacca7";
        long versionKey = 1491826292;
        CatInfoType[] categories;
        //s515d776

        serviceService service;
        //     serviceService service;
         // uchwyt sesji jako zmienna globalna


        public MainWindow()
        {
            //       service = new serviceService();
            service = new serviceService();
            //     AllegroWebApiService service;
           

            

            InitializeComponent();
            //if (dc.DatabaseExists())
            //{
            //    dataGrid.ItemsSource = dc.Cards.Where(x=>x.Category.Contains(textBox.Text) );

            //}
        }

        public MainWindow(string _sessionHandl)
        {
            //       service = new serviceService();
            service = new serviceService();
            //     AllegroWebApiService service;
            sessionHandle = _sessionHandl;

            InitializeComponent();
        generate_columns();
            //if (dc.DatabaseExists())
            //{
            //    dataGrid.ItemsSource = dc.Cards.Where(x=>x.Category.Contains(textBox.Text) );
       

            //}
        }
        private void generate_columns()
        {
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Allegro Id";
            c1.Binding = new Binding("itemId");
            c1.Width = 80;
            dataGrid.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "Title";
            c2.Width = 330;
            c2.Binding = new Binding("itemTitle");
            dataGrid.Columns.Add(c2);
            DataGridTextColumn c9 = new DataGridTextColumn();
            c9.Header = "condition Info";
            c9.Width = 50;
            c9.Binding = new Binding("conditionInfo");//ile zostało kupionych
            dataGrid.Columns.Add(c9);
            DataGridTextColumn c10 = new DataGridTextColumn();
            c10.Header = "Price Type";
            c10.Width = 60;
            c10.Binding = new Binding("priceInfo[0].priceType");
            dataGrid.Columns.Add(c10);
            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "price";
            c3.Width = 50;
            c3.Binding = new Binding("priceInfo[0].priceValue");
            dataGrid.Columns.Add(c3);
            DataGridTextColumn c4 = new DataGridTextColumn();
            c4.Header = "left Count";
            c4.Width = 30;
            c4.Binding = new Binding("leftCount");//ile zostało do kupienia
            dataGrid.Columns.Add(c4);
            DataGridTextColumn c5 = new DataGridTextColumn();
            c5.Header = "bids Count";
            c5.Width = 60;
            c5.Binding = new Binding("bidsCount");//ile zostało kupionych
            dataGrid.Columns.Add(c5);
            DataGridTextColumn c6 = new DataGridTextColumn();
            c6.Header = "bidders Count";
            c6.Width = 60;
            c6.Binding = new Binding("biddersCount");//ile zostało kupionych
            dataGrid.Columns.Add(c6);
            DataGridTextColumn c7 = new DataGridTextColumn();
            c7.Header = "quantity Type";
            c7.Width = 50;
            c7.Binding = new Binding("quantityType");//ile zostało kupionych
            dataGrid.Columns.Add(c7);
            DataGridTextColumn c8 = new DataGridTextColumn();
            c8.Header = "ending Time";
            c8.Width = 120;
            c8.Binding = new Binding("endingTime");//ile zostało kupionych
            dataGrid.Columns.Add(c8);

       

        }






        private void listaKategorii_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListaRodzicow.Items.Clear();
            var tmpCat = categories[listaKategorii.SelectedIndex].catId;
            var listapodkategorii = categories.Where(x => x.catParent == tmpCat);
            foreach (var item in listapodkategorii)
            {
                ListaRodzicow.Items.Add(item.catName);
            }
        }
        private void WczytajKategorie()
        {

            string verStr;
            long verKey;
             categories = service.doGetCatsData(1, 0, true, webapiKey, out verKey, out verStr);

            
         var a = categories.Where(x => x.catParent == 0);
            foreach (var item in a)
            {
                listaKategorii.Items.Add(item.catName);
            }
     
        //    foreach (var item in categories)
        //    {
                

        //        //     if (i==1)
        //        //     {

     
 
        ////            ListaRodzicow.Items.Add(+item.catId + " " + item.catParent + "          " + item.catName + " " + item.catPosition);
         


        //        //         i = 2;
        //        //     }    
        //    }
  
        }
        private void PobierzDane()
        {


            FilterOptionsType[] filter = new FilterOptionsType[6];
            filter[0] = new FilterOptionsType();
            filter[1] = new FilterOptionsType();
            filter[2] = new FilterOptionsType();
            filter[2].filterValueRange = new RangeValueType();

            //filter[0].filterId = "search";
            //filter[1].filterId = "offerType";

      
            //       filter[2].filterId = "category";
            //   filter[3].filterId = "price";
            string[] filterValue_Search = new string[1];
            filterValue_Search[0] = Tittle_TextBox.Text;
            string[] filterValue_OfferType = new string[2];
            filterValue_OfferType[0] = "buyNow";
            filterValue_OfferType[1] = "auction";


            string[] filterValue_price = new string[2];
            filterValue_price[0] = "0";
            filterValue_price[1] = "1000000";
            string[] filterValue_City = new string[1];
            filterValue_City[0] = Country_TextBox.Text;

            string[] filterValue_PostCode = new string[1];
            filterValue_PostCode[0] = "43-300";
            string[] filterValue_Distance = new string[1];
            filterValue_Distance[0] = "1000000km";
            if (MinPrice_TextBox.Text!="")
            {
                filterValue_price[0] = MinPrice_TextBox.Text;     
            }
            if (MaxPrice_TextBox.Text!="")
            {
                filterValue_price[1] = MaxPrice_TextBox.Text;
            }

       

            filter[0].filterId = "search";
            filter[1].filterId = "offerType";
            filter[2].filterId = "price";
         

            int i = 3;
            if (Distance_TextBox.Text != "")
            {
                filter[i] = new FilterOptionsType();
       
                filter[i].filterId = "postCode";
                filter[i].filterValueId = filterValue_PostCode;
                ++i;
                filterValue_Distance[0] = (Distance_TextBox.Text + "km");
                filter[i] = new FilterOptionsType();
                filter[i].filterId = "distance";
                filter[i].filterValueId = filterValue_Distance;
                ++i;
            }
            if (Country_TextBox.Text != "")
            {
                filter[i] = new FilterOptionsType();
                filterValue_City[0] = Country_TextBox.Text;
                filter[i].filterId = "city";
                filter[i].filterValueId = filterValue_City;
                ++i;
            }

            filter[0].filterValueId = filterValue_Search;
            filter[1].filterValueId = filterValue_OfferType;
            filter[2].filterValueRange.rangeValueMin= filterValue_price[0];
            filter[2].filterValueRange.rangeValueMax = filterValue_price[1];
          
  
            int itemsFeaturedCount; // Liczba promowanych ofert
            bool itemsFeaturedCountSpecified;
            ItemsListType[] itemsList; // Oferty
            CategoriesListType categoriesList; // Informacje o kategoriach

            //CategoryData categoriesListl;
            //FiltersListType[] filtersList; // Filtry możliwe do ustawienia
            string[] filtersRejected; // Niepoprawne filtry

            //sessionHandle - przechowuję liczbę wszystkich zwróconych ofert (pasujacych do zapytania)
            //       service.doGetItemsInfo(,);
            //var a = service.doGetItemsList(
            //                      webapiKey, // WebApi klucz
            //                      1, // Id kraju => 1 dla PL
            //                      filter, // Kryteria filtrowania
            //                      null, // Kryteria sortowania
            //                      1000, // Rozmiar porcji wynikowej, domyślnie (przy false pobiera 100)
            //                      true, // 
            //                      0, // Sterowanie pobieraniem kolejnych porcji
            //                      false, //
            //                      0, // Sterowanie zakresem zwracanych danych
            //                      false, //
            //                      out itemsFeaturedCount, out itemsFeaturedCountSpecified, out itemsList, out categoriesList, out filtersList, out filtersRejected);



            FiltersListType[] filterList;
            service.doGetItemsList(
                                  webapiKey, // WebApi klucz
                                  1, // Id kraju => 1 dla PL
                                  filter, // Kryteria filtrowania
                                  null, // Kryteria sortowania
                                  1000, // Rozmiar porcji wynikowej, domyślnie (przy false pobiera 100)
                                  true, // 
                                  0, // Sterowanie pobieraniem kolejnych porcji
                                  false, //
                                  0, // Sterowanie zakresem zwracanych danych
                                  false, //
                                  out itemsFeaturedCount, out itemsFeaturedCountSpecified, out itemsList, out categoriesList, out filterList, out filtersRejected);
            string str_parameters;

         


            dataGrid.ItemsSource = itemsList;
            itemsList2 = itemsList;

 



            //service.doShowItemInfoExt(sessionHandle,);
            //   dataGrid.Items.Add(itemsList);
            //foreach (var item in itemsList)
            //{
            //    str_parameters = "";
            //    if (item.parametersInfo != null)
            //    {
            //        foreach (var parametr in item.parametersInfo)
            //        {
            //            str_parameters += parametr.parameterValue;
            //        }
            //    }

            //    //    dataGrid.Items.Add(item.itemId + item.itemTitle + item.sellerInfo + str_parameters);

            //}

        }
  
        private void LoginBtn_Click(object sender, EventArgs e)
        {


            //  Zaloguj();
            PobierzDane();


        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            PobierzDane();
            //   dataGrid.ItemsSource = dc.Cards.Where(x => x.Category.Contains(textBox.Text));

            //          dataGrid.ItemsSource = dc.Users;
        }
        private void MMMENNUU_Click(object sender, RoutedEventArgs e)
        {
     int liczba=       service.doMyAccountItemsCount(sessionHandle, "watch", null);
            //  var  Itwatches=    service.doMyAccount2(sessionHandle, "watch", 0, true,null, 10, false);
            // var prod=      Itwatches[0].myAccountArray[0];

           
            int licznik = 0;
        }


        // private void FillDataGrid()
        // {
        ////     string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
        //     string CmdString = string.Empty;
        //     using (SqlConnection con = new SqlConnection("System.Data.SqlClient"))
        //     {
        //         CmdString = "SELECT* FROM Card";
        //         SqlCommand cmd = new SqlCommand(CmdString, con);
        //         SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //         DataTable dt = new DataTable("Card");
        //         sda.Fill(dt);
        //         dataGrid.ItemsSource = dt.DefaultView;
        //     }
        // }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
 
           SelectedItem = itemsList2[dataGrid.SelectedIndex];
        }

        private void button_AddView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemView a = new ItemView(SelectedItem,sessionHandle);
                a.Show();
            }
            catch (Exception)
            {

            }

        }
        private void button_Return_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            this.Close();
            menu.Show();
        }

        private void LoadCat_btn_Click(object sender, RoutedEventArgs e)
        {
            WczytajKategorie();
        }
        private void Country_TextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (Country_TextBox.Text!="")
            {
                Distance_TextBox.IsEnabled = false;
            }

        }
    }
}
