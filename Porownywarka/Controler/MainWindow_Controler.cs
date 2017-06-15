using Porownywarka.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Porownywarka.View;
using Porownywarka;
using Porownywarka.Controler;

namespace Porownywarka
{
    public partial class MainWindow : Window
    {

        private bool Update;
        public int User;
        public int activeParameter;
        public ItemsListType[] itemsList2;
        public ItemsListType SelectedItem;
        public string sessionHandle;

        const string webapiKey = "abfacca7";

        long versionKey = 1491826292;
        public int ToUpdate;
        CatInfoType[] categories;
        //s515d776

        serviceService service;

        public MainWindow(int user)
        {
         
            Update=false;
            service = new serviceService();
            InitializeComponent();
            generate_columns();
            User = user;
        }
        public MainWindow(int user,SearchingParameter SearchinActiveParameter)
        {
            ToUpdate = SearchinActiveParameter.Id;
            Update = true;
            service = new serviceService();
            InitializeComponent();
            generate_columns();
            User = user;
            activeParameter = SearchinActiveParameter.Id;
            AddSearch.Content = "Update";
           
            var searchingParameter = DatabaseConnection.dc.SearchingParameters.First(x => x.Id == activeParameter);
            Country_TextBox.Text = searchingParameter.Country;
            Tittle_TextBox.Text = searchingParameter.Tittle;
            MaxPrice_TextBox.Text = searchingParameter.MaxPrice;
            MinPrice_TextBox.Text = searchingParameter.MinPrice;
            Distance_TextBox.Text = searchingParameter.Distance;
            Category_TextBox.Text = searchingParameter.CategoryID.ToString();
        }


        public MainWindow(string _sessionHandl)
        {
            service = new serviceService();
            sessionHandle = _sessionHandl;
            Update = false;
            InitializeComponent();
             generate_columns();
        }
        private void generate_columns()
        {
            var c1 = new DataGridTextColumn
            {
                Header = "Allegro Id",
                Binding = new Binding("itemId"),
                Width = 80
            };
            dataGrid.Columns.Add(c1);
            var c2 = new DataGridTextColumn
            {
                Header = "Title",
                Width = 330,
                Binding = new Binding("itemTitle")
            };
            dataGrid.Columns.Add(c2);
            var c9 = new DataGridTextColumn
            {
                Header = "condition Info",
                Width = 50,
                Binding = new Binding("conditionInfo")
            };

            dataGrid.Columns.Add(c9);
            var c10 = new DataGridTextColumn
            {
                Header = "Price Type",
                Width = 60,
                Binding = new Binding("priceInfo[0].priceType")
            };
            dataGrid.Columns.Add(c10);
            var c3 = new DataGridTextColumn
            {
                Header = "price",
                Width = 50,
                Binding = new Binding("priceInfo[0].priceValue")
            };
            dataGrid.Columns.Add(c3);
            var c4 = new DataGridTextColumn
            {
                Header = "left Count",
                Width = 30,
                Binding = new Binding("leftCount")
            };

            dataGrid.Columns.Add(c4);
            var c5 = new DataGridTextColumn
            {
                Header = "bids Count",
                Width = 60,
                Binding = new Binding("bidsCount")
            };
   
            dataGrid.Columns.Add(c5);
            var c6 = new DataGridTextColumn
            {
                Header = "bidders Count",
                Width = 60,
                Binding = new Binding("biddersCount")
            };
            dataGrid.Columns.Add(c6);
            var c7 = new DataGridTextColumn
            {
                Header = "quantity Type",
                Width = 50,
                Binding = new Binding("quantityType")
            };

            dataGrid.Columns.Add(c7);
            var c8 = new DataGridTextColumn
            {
                Header = "ending Time",
                Width = 120,
                Binding = new Binding("endingTime")
            };
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
    
        }
        private void PobierzDane()
        {


            var filter = new FilterOptionsType[6];
            filter[0] = new FilterOptionsType();
            filter[1] = new FilterOptionsType();
            filter[2] = new FilterOptionsType {filterValueRange = new RangeValueType()};
            var filterValue_Search = new string[1];
            filterValue_Search[0] = Tittle_TextBox.Text;
            var filterValue_OfferType = new string[2];
            filterValue_OfferType[0] = "buyNow";
            filterValue_OfferType[1] = "auction";


            var filterValue_price = new string[2];
            filterValue_price[0] = "0";
            filterValue_price[1] = "1000000";
            var filterValue_City = new string[1];
            filterValue_City[0] = Country_TextBox.Text;

            var filterValue_PostCode = new string[1];
            filterValue_PostCode[0] = "43-300";
            var filterValue_Distance = new string[1];
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

            var i = 3;
            if (Distance_TextBox.Text != "")
            {
                filter[i] = new FilterOptionsType
                {
                    filterId = "postCode",
                    filterValueId = filterValue_PostCode
                };

                ++i;
                filterValue_Distance[0] = (Distance_TextBox.Text + "km");
                filter[i] = new FilterOptionsType
                {
                    filterId = "distance",
                    filterValueId = filterValue_Distance
                };
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
                                  out itemsFeaturedCount, out itemsFeaturedCountSpecified, out itemsList, out categoriesList, out filterList, out filtersRejected);
            string str_parameters;
            dataGrid.ItemsSource = itemsList;
            itemsList2 = itemsList;
        }
  
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            //  Zaloguj();
            PobierzDane();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            PobierzDane();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           SelectedItem = itemsList2[dataGrid.SelectedIndex];
        }
        private void button_AddView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var a = new ItemView(SelectedItem,sessionHandle);
                a.Show();
            }
            catch (Exception)
            {
                // sessionHandle is null
            }
        }
        private void button_Return_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
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
        private void AddSearching(object sender, RoutedEventArgs e)
        {
            if (Update)
            {
                var seachingParameter = new SearchingParameter();

                if ((MaxPrice_TextBox.Text) != "")
                {
                    seachingParameter.MaxPrice = (MaxPrice_TextBox.Text);
                }
                if ((MinPrice_TextBox.Text) != "")
                {
                    seachingParameter.MinPrice = (MinPrice_TextBox.Text);
                }
                if (Tittle_TextBox.Text != "")
                {
                    seachingParameter.Tittle = Tittle_TextBox.Text;
                }
                if (Distance_TextBox.Text != "")
                {
                    seachingParameter.Distance = Distance_TextBox.Text + "km";
                }
                else
                {
                    if ((Country_TextBox.Text) != "")
                    {
                        seachingParameter.Country = (Country_TextBox.Text);
                    }
                }
                if ((Category_TextBox.Text) != "")
                {
                    seachingParameter.CategoryID = Convert.ToInt32(Category_TextBox.Text);
                }
                if (Update == false)
                {
                    seachingParameter.Id = DatabaseConnection.dc.SearchingParameters.Max(x => x.Id) + 1;
                    seachingParameter.CustomerID = User;
                    DatabaseConnection.dc.SearchingParameters.InsertOnSubmit(seachingParameter);
                    DatabaseConnection.dc.SubmitChanges();
                }
                else
                {
                    var item = DatabaseConnection.dc.SearchingParameters.First(x => x.Id == ToUpdate);
                    seachingParameter.CustomerID = User;
                    item = seachingParameter;
                    DatabaseConnection.dc.SubmitChanges();
                }
            }
            else
            {


                var seachingParameter = new SearchingParameter();
           
                if ((MaxPrice_TextBox.Text) != "")
                {
                    seachingParameter.MaxPrice = (MaxPrice_TextBox.Text);
                }
                if ((MinPrice_TextBox.Text) != "")
                {
                    seachingParameter.MinPrice = (MinPrice_TextBox.Text);
                }
                if (Tittle_TextBox.Text != "")
                {
                    seachingParameter.Tittle = Tittle_TextBox.Text;
                }
                if (Distance_TextBox.Text != "")
                {
                    seachingParameter.Distance = Distance_TextBox.Text + "km";
                }
                else
                {
                    if ((Country_TextBox.Text) != "")
                    {
                        seachingParameter.Country = (Country_TextBox.Text);
                    }
                }
                if ((Category_TextBox.Text) != "")
                {
                    seachingParameter.CategoryID = Convert.ToInt32(Category_TextBox.Text);
                }
                if (Update == false)
                {
                    seachingParameter.Id = DatabaseConnection.dc.SearchingParameters.Max(x => x.Id) + 1;
                    seachingParameter.CustomerID = User;
                    DatabaseConnection.dc.SearchingParameters.InsertOnSubmit(seachingParameter);
                    DatabaseConnection.dc.SubmitChanges();
                }
                else
                {

                    var item = DatabaseConnection.dc.SearchingParameters.First(x => x.Id == ToUpdate);
                    seachingParameter.CustomerID = User;
                    item = seachingParameter;
                    DatabaseConnection.dc.SubmitChanges();
                }
            }
            var searched=new Searched(User);
            searched.Show();
            this.Close();
        
        }

    }
}
