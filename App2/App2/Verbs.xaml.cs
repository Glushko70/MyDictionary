using System.Collections.Generic;


using Xamarin.Forms;

namespace App2
{
    public partial class Verbs : ContentPage
    {
        List<MyLibrary> temp = new List<MyLibrary>();
        public Verbs()
        {
            Title = "Неправильні дієслова";            
            loadLibrary();
            
            ListView listView = new ListView
            {               
                ItemsSource = temp,
                ItemTemplate=new DataTemplate(() =>
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
                            Children = { nameLabel,nameLabel1}                           
                        }
                    };
                })
            };
            Label font = new Label();
            listView.RowHeight = (int) (font.FontSize*2) + 20;           
            listView.ItemSelected += listView_ItemSelected;
            this.Content = new StackLayout { Children = { listView } };
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool result = await DisplayAlert("Виберіть дію", "Добавити до карточок?", "Так", "Ні");
            if (result)
            {
                MyLibrary item = new MyLibrary();
                item = (MyLibrary)e.SelectedItem;
                await Navigation.PushAsync(new AddWord(item,"1.txt"));
            }
        }
        private async void loadLibrary()
        {
            temp= await DependencyService.Get<IFileWorker>().LoadTextAsync("1.txt");
        }
    }
}
