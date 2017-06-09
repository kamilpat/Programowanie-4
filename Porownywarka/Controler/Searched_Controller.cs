using Porownywarka.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Porownywarka.View
{
    public partial class Searched 
    {
        public static LINQToSQLClassDataContext dc = new LINQToSQLClassDataContext(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Janusz\Desktop\Programownie\Projekt\Porownywarka\Porownywarka\SearchedEngineDataBase.mdf;Integrated Security = True; Connect Timeout = 30");

        public int ActiveUser;
        public int IDSearching_Parameter = 0;
        public long IDDecission = 0;
        public SearchingParameter[] Searching;
        public ItemsListType[] ItemsListProducts;
        public Product[] ObservedItemsListProducts;
        public Product[] Products;
        public string sessionHandle;
        const string webapiKey = "abfacca7";
        long versionKey = 1491826292;

        serviceService service;

        public Searched(int user)
        {

            ActiveUser = user;
            service = new serviceService();
            InitializeComponent();
            FillSeaching();
        }

        private void FillSeaching()
        {
            Searched_datagrid.ItemsSource = dc.SearchingParameters.Where(x => x.CustomerID == ActiveUser).ToArray();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(ActiveUser);
            mainWindow.Show();
            this.Hide();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(ActiveUser, (SearchingParameter)Searched_datagrid.SelectedItem);
            mainWindow.Show();
            this.Hide();
        }

        private void DeleteSearched_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Searched_datagrid.SelectedItem == null) return;
            dc.SearchingParameters.DeleteOnSubmit((SearchingParameter)Searched_datagrid.SelectedItem);
            dc.SubmitChanges();
            FillSeaching();
        }

        private void PobierzDane(SearchingParameter Searching)
        {


            var filter = new FilterOptionsType[6];
            filter[0] = new FilterOptionsType();
            filter[1] = new FilterOptionsType();
            filter[2] = new FilterOptionsType();
            filter[2].filterValueRange = new RangeValueType();


            var filterValue_Search = new string[1];
            filterValue_Search[0] = Searching.Tittle.Trim();
            var filterValue_OfferType = new string[2];
            filterValue_OfferType[0] = "buyNow";
            filterValue_OfferType[1] = "auction";


            var filterValue_price = new string[2];
            filterValue_price[0] = "0";
            filterValue_price[1] = "1000000";
            var filterValue_City = new string[1];
            if (Searching.Country != null)
            {
                filterValue_City[0] = Searching.Country.Trim();
            }


            var filterValue_PostCode = new string[1];
            filterValue_PostCode[0] = "43-300";
            var filterValue_Distance = new string[1];
            filterValue_Distance[0] = "1000000km";
            if (Searching.MinPrice != null && Searching.MinPrice.Trim() != "")
            {
                filterValue_price[0] = Searching.MinPrice.Trim();
            }
            if (Searching.MaxPrice != null && Searching.MaxPrice.Trim() != "")
            {
                filterValue_price[1] = Searching.MaxPrice.Trim();
            }
            filter[0].filterId = "search";
            filter[1].filterId = "offerType";
            filter[2].filterId = "price";
            var i = 3;
            if (Searching.Distance != null && Searching.Distance.Trim() != "km")
            {
                filter[i] = new FilterOptionsType
                {
                    filterId = "postCode",
                    filterValueId = filterValue_PostCode
                };

                ++i;
                filterValue_Distance[0] = (Searching.Distance.Trim() + "km");
                filter[i] = new FilterOptionsType
                {
                    filterId = "distance",
                    filterValueId = filterValue_Distance
                };
                ++i;
            }
            if (Searching.Country != null && Searching.Country.Trim() != "")
            {
                filter[i] = new FilterOptionsType();
                filterValue_City[0] = Searching.Country.Trim();
                filter[i].filterId = "city";
                filter[i].filterValueId = filterValue_City;
                ++i;
            }

            filter[0].filterValueId = filterValue_Search;
            filter[1].filterValueId = filterValue_OfferType;
            filter[2].filterValueRange.rangeValueMin = filterValue_price[0];
            filter[2].filterValueRange.rangeValueMax = filterValue_price[1];


            int itemsFeaturedCount; // Liczba promowanych ofert
            bool itemsFeaturedCountSpecified;
            ItemsListType[] itemsList; // Oferty
            CategoriesListType categoriesList; // Informacje o kategoriach
            string[] filtersRejected; // Niepoprawne filtry

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
                out itemsFeaturedCount, out itemsFeaturedCountSpecified, out itemsList, out categoriesList,
                out filterList, out filtersRejected);

            ItemsListProducts = itemsList;
            if (itemsList == null) return;
            var a = 0;
            Products = new Product[itemsList.Length];
            var quantity = 1;
            var count = 1;
            if (!dc.Products.Any())
            {
                count = 1;
            }
            else
            {
                count = dc.Products.Max(x => x.ID) + 1;
            }
            quantity = count;
            foreach (var item in itemsList)
            {

                var elemet = new Product
                {
                    ID = count,
                    IDItem = item.itemId,
                    Title = item.itemTitle,
                    Condition = item.conditionInfo,
                    Price = item.priceInfo[0].priceValue,
                    PriceType = item.priceInfo[0].priceType,
                    Left = item.leftCount,
                    Bids_Count = item.bidsCount,
                    Bidders_Count = item.biddersCount,
                    Quantity = item.quantityType,
                    EndingTime = item.endingTime.ToString(CultureInfo.InvariantCulture)
                };

                Products[a] = elemet;
                a++;
                count++;
            }
            var lista = new ListOfProduct[Products.Length];
            var IdOfList = 0;
            if (!dc.ListOfProducts.Any())
            {
                IdOfList = 1;
            }
            else
            {
                IdOfList = dc.ListOfProducts.Max(x => x.IDListOfProducts) + 1;
            }
            //funktion
            var nazwa = dc.Products
                             .Join(dc.ListOfProducts,
                                 post => (long)post.IDItem
                                 ,
                                 meta => (long)meta.IDItem,
                                 (post, meta) => new
                                 {
                                     Post = post,
                                     Meta = meta
                                 })
                             .Where(postAndMeta => postAndMeta.Meta.Status == 0 && postAndMeta.Meta.IDCustomer == ActiveUser).ToArray(); // where statement
            List<Product> ListProducts = new List<Product>();
            foreach (var VARIABLE in nazwa)
            {
                ListProducts.Add(VARIABLE.Post);
            }
            Decision_datagrid.ItemsSource = ListProducts;
            //funktion
            var licznik = 0;
            if (quantity == 1)
            {
                foreach (var Item in Products)
                {
                    dc.Products.InsertOnSubmit(Item);
                    dc.SubmitChanges();
                    lista[licznik] = new ListOfProduct();
                    lista[licznik].IDCustomer = ActiveUser;
                    lista[licznik].IDListOfProducts = IdOfList;
                    lista[licznik].IDListOfProducts = IdOfList;
                    lista[licznik].IDItem = Item.IDItem;
                    dc.ListOfProducts.InsertOnSubmit(lista[licznik]);
                    dc.SubmitChanges();
                    IdOfList++;
                    licznik++;
                }
            }
            else
            {
                foreach (var Item in Products)
                {
                    if (!dc.Products.Any(x => x.IDItem == Item.IDItem))
                    {
                        dc.Products.InsertOnSubmit(Item);
                        dc.SubmitChanges();
                    }
                    if (dc.ListOfProducts.Any(x => x.IDItem == Item.IDItem && x.IDCustomer == ActiveUser)) continue;
                    lista[licznik] = new ListOfProduct();
                    lista[licznik].IDCustomer = ActiveUser;
                    lista[licznik].IDListOfProducts = IdOfList;
                    lista[licznik].IDListOfProducts = IdOfList;
                    lista[licznik].IDItem = Item.IDItem;
                    dc.ListOfProducts.InsertOnSubmit(lista[licznik]);
                    dc.SubmitChanges();
                    IdOfList++;
                    licznik++;
                }
            }

            DisplayObserved();
        }
        public void DisplayObserved()
        {
            var Tablica = dc.ListOfProducts.Where(x => x.IDCustomer == ActiveUser && x.Status == 1).ToArray();
            ObservedItemsListProducts = new Product[Tablica.Length];
            for (int i = 0; i < Tablica.Length; i++)
            {
                ObservedItemsListProducts[i] = new Product();
            }
            FillObserved();
            FillRefused();
        }


        private void AddToInterest_Click(object sender, RoutedEventArgs e)
        {
            dc.ListOfProducts.Single(x => x.IDItem == IDDecission && x.IDCustomer == ActiveUser).Status = 1;
            dc.SubmitChanges();
          FillObserved();
        }
        private void FillDecision() {
            var nazwa = dc.Products
                       .Join(dc.ListOfProducts,
                           post => (long)post.IDItem
                           ,
                           meta => (long)meta.IDItem,
                           (post, meta) => new
                           {
                               Post = post,
                               Meta = meta
                           })
                       .Where(postAndMeta => postAndMeta.Meta.Status == 0 && postAndMeta.Meta.IDCustomer == ActiveUser).ToArray(); // where statement
            List<Product> ListProducts = new List<Product>();
            foreach (var VARIABLE in nazwa)
            {
                ListProducts.Add(VARIABLE.Post);
            }
            Decision_datagrid.ItemsSource = ListProducts;
        }
        private void Delete_SearcButton_Click(object sender, RoutedEventArgs e)
        {
            dc.ListOfProducts.Single(x => x.IDItem == IDDecission && x.IDCustomer == ActiveUser).Status = 2;
            dc.SubmitChanges();
            FillRefused();
            FillDecision();
        }

        private void ResetRejected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            var rejectedReset=dc.ListOfProducts.Where(x => x.IDCustomer == ActiveUser&&x.Status==2) ;
                foreach (var item in rejectedReset)
                {
                    item.Status = 0;
                }
                dc.SubmitChanges();
                FillDecision();
         
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        private void FillObserved()
        {
            try
            {
                var query = dc.Products
                    .Join(dc.ListOfProducts,
                        post => (long)post.IDItem
                        ,
                        meta => (long)meta.IDItem,
                        (post, meta) => new
                        {
                            Post = post,
                            Meta = meta
                        })
                    .Where(postAndMeta => postAndMeta.Meta.Status == 1 && postAndMeta.Meta.IDCustomer == ActiveUser).ToArray(); // where statement

                var obsList = new List<Product>();
                foreach (var VARIABLE in query)
                {
                    obsList.Add(VARIABLE.Post);
                }
                Observed_dataGrid.ItemsSource = obsList;
            }
            catch (Exception e)
            {

            }

        }
        private void FillRefused()
        {
            try
            {

                var query = dc.Products // your starting point - table in the "from" statement
                    .Join(dc.ListOfProducts, // the source table of the inner join
                        post => (long)post
                            .IDItem, // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                        meta => (long)meta.IDItem, // Select the foreign key (the second part of the "on" clause)
                        (post, meta) => new
                        {
                            Post = post,
                            Meta = meta
                        }) // selection
                    .Where(postAndMeta => postAndMeta.Meta.Status == 2 && postAndMeta.Meta.IDCustomer == ActiveUser)
                    .ToArray(); // where statement
                var refuseList = new List<Product>();
                foreach (var VARIABLE in query)
                {
                    refuseList.Add(VARIABLE.Post);
                }
                Rejected_datagrid.ItemsSource = refuseList;
            }
            catch (Exception e)
            {

            }
        }



        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void DeleteObserved_btn_OnClick_Click(object sender, RoutedEventArgs e)
        {
            dc.ListOfProducts.First(x => x.IDCustomer == ActiveUser).Status = 0;
            dc.SubmitChanges();
        }

        private void Searched_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tempSearch = Searched_datagrid.SelectedItem;
            if (tempSearch != null)
            {
                PobierzDane((SearchingParameter)tempSearch);
            }
        }

        private void Decision_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = (Product)Decision_datagrid.SelectedItem;
            IDDecission = index.IDItem;
        }
    }
}
