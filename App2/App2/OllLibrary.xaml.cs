using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace App2
{
    public partial class OllLibrary : ContentPage
    {
        ObservableCollection<MyLibrary> temp = new ObservableCollection<MyLibrary>();
        int language = 0;
        Entry search = new Entry();
        Button searchLanguage = new Button();
        string search1 = "";
        public OllLibrary()
        {
            Title = "Мій словник";
            InitializeComponent();            
            searchLanguage.Text = "English";
            searchLanguage.BackgroundColor = Color.Yellow;
            searchLanguage.TextColor = Color.Blue;          
            search.Placeholder = "Пошук"+" "+searchLanguage.Text;
            searchLanguage.Clicked += SearchLanguage_Clicked;
            search.TextChanged += Search_TextChanged;            
        }
        /// <summary>
        /// Вибір мови пошуку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchLanguage_Clicked(object sender, EventArgs e)
        {
            if(language==0)
            {
                searchLanguage.Text = "Українська";
                language = 1;
            }
            else
            {
                searchLanguage.Text = "English";                             
                language = 0;
            }
            search.Placeholder = "Пошук" + " " + searchLanguage.Text;
        }
        /// <summary>
        /// Пошук слова в моєму словнику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text == "")
                searchLanguage.IsEnabled = true;
            else
                searchLanguage.IsEnabled = false;
            string compa2 = "";
            var searchTemp = new ObservableCollection<MyLibrary>();
            var entry = sender as Entry;
            search1 = entry.Text;
            string compa1 = search1.ToLower();
            for (int i = 0; i < temp.Count; i++)
            {
                if (language == 0)
                    compa2 = temp[i].wordLanguage1.ToLower();
                else
                    compa2 = temp[i].wordLanguage2.ToLower();
                if (compa2.StartsWith(compa1))
                    searchTemp.Add(temp[i]);
            }
            (BindingContext as LibraryViewModel).UpdateList(searchTemp);
        }

        /// <summary>
        /// Вывод всего словаря в ListViev
        /// </summary>        
        public void Show()
        {
            ListView listView = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "wordLanguage1");
                    Label nameLabel1 = new Label();
                    nameLabel1.BackgroundColor = Color.Silver;
                    nameLabel1.SetBinding(Label.TextProperty, "wordLanguage2");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(5, 0),
                            Orientation = StackOrientation.Vertical,
                            Children = { nameLabel, nameLabel1 }
                        }
                    };
                })
            };
            listView.SetBinding(ListView.ItemsSourceProperty, new Binding("Mylibrary"));
            listView.ItemSelected += listView_ItemSelected;
            Label font = new Label();
            listView.RowHeight = (int)(font.FontSize * 2) + 20;
            this.Content = new StackLayout { Children = { searchLanguage,search, listView } };
        }        

        /// <summary>
        /// Перенос слова со словаря в рабочие карточки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool result = await DisplayAlert("Виберіть дію", "Перенести до карточок?", "Так", "Ні");
            if (result)
            {
                MyLibrary item = new MyLibrary();
                item = (MyLibrary)e.SelectedItem;                            
                await Navigation.PushAsync(new AddWord(item, "olllibrary"));
                search.Text = "";                          
            }
        }
        protected override void OnAppearing()
        {
            this.BindingContext = new LibraryViewModel();
            temp = (BindingContext as LibraryViewModel).Mylibrary;            
            Show();            
        }
    }
}
