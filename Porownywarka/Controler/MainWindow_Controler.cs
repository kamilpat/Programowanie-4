using Porownywarka.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Porownywarka
{
    public partial class MainWindow : Window
    {
        public ItemsListType[] itemsList2;
        public ItemsListType SelectedItem;
        ////    LINQToSQLClassDataContext dc = new LINQToSQLClassDataContext(Properties.Settings.Default.GraphicCardConnectionString);
        const string webapiKey = "";
        long versionKey = 1491826292;
        //s515d776

        serviceService service;
        //     serviceService service;
        string sessionHandle;   // uchwyt sesji jako zmienna globalna


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
        private void WczytajKategorie()
        {

            string verStr;
            long verKey;
            var categories = service.doGetCatsData(1, 0, true, webapiKey, out verKey, out verStr);

            foreach (var item in categories)
            {
                listaKategorii.Items.Add(item.catName);
            }
        }
        private void PobierzDane()
        {


            FilterOptionsType[] filter = new FilterOptionsType[2];
            filter[0] = new FilterOptionsType();
            filter[1] = new FilterOptionsType();

            //filter[0].filterId = "search";
            //filter[1].filterId = "offerType";

            filter[0].filterId = "search";
            filter[1].filterId = "offerType";
            //       filter[2].filterId = "category";
            //   filter[3].filterId = "price";
            string[] filterValue_Search = new string[1];
            filterValue_Search[0] = Tittle_TextBox.Text;//TBoxItemName.Text.ToString();
            string[] filterValue_OfferType = new string[1];
            filterValue_OfferType[0] = "buyNow";
            //filterValue_OfferType[1] = "auction";

            //filter[0].filterValueId = filterValue_Search;

            //filter[1].filterValueId = filterValue_OfferType;
            filter[0].filterValueId = filterValue_Search;
            filter[1].filterValueId = filterValue_OfferType;

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

            var x = "a";
            //   dataGrid.ItemsSource = itemsList;

            dataGrid.ItemsSource = itemsList;
            itemsList2 = itemsList;

            //    WczytajKategorie();
            //     textBlock.Text = itemsList[0].categoryId.ToString();// + itemsList[0].additionalInfo.ToString() + itemsList[0].advertInfo.ToString() + itemsList[0].biddersCount.ToString() + itemsList[0].bidsCount.ToString() + itemsList[0].conditionInfo.ToString();
            //       textBlock.Text = itemsList[0].itemTitle.ToString(); TYTUŁ
            //           textBlock.Text = itemsList[0].conditionInfo.ToString(); //stan używaany , nowy itd
            //   textBlock.Text = itemsList[0].sellerInfo.ToString(); uzywane
            //         textBlock.Text = itemsList[0].sellerInfo.userLogin;
            //textBlock.Text = itemsList[0].sellerInfo.countryId.ToString(); identyfikator id
            // textBlock.Text = itemsList[0].photosInfo[0].photoUrl;



            //    image.Source = new BitmapImage(new Uri(itemsList[0].photosInfo[0].photoUrl, UriKind.Relative));



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



            //  dataGrid.Items.Add("session handle: " + sessionHandle);   //po to stworzyliśmy logLbx, żeby teraz sobie w nim wyświetlać komunikaty. 
            //   dataGrid.Items.Add("hash offset: " + hashoffset.ToString());//Wyświetlamy więc wszystkie zmienne.
            //    dataGrid.Items.Add("server time: " + serverTime.ToString());
            //  Zaloguj();
            PobierzDane();


        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            PobierzDane();
            //   dataGrid.ItemsSource = dc.Cards.Where(x => x.Category.Contains(textBox.Text));






            //          dataGrid.ItemsSource = dc.Users;
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
                ItemView a = new ItemView(SelectedItem);
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
    }
}
