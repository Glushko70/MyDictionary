using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "Меню";
            InitializeComponent();
            button1.BackgroundColor = button2.BackgroundColor = button3.BackgroundColor = Color.Aqua;
            button4.BackgroundColor = button5.BackgroundColor = Color.Aqua;
            stackLayout.BackgroundColor = Color.Default;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddWord());
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            if (DependencyService.Get<IFileWorker>().ExistsAsync("cardsLibrary").Result)
                await Navigation.PushAsync(new Card());
            else
                await DisplayAlert("Ви не ввели жодного слова", "Добавте слово", "Ok");
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            if (DependencyService.Get<IFileWorker>().ExistsAsync("cardsLibrary").Result)
                await Navigation.PushAsync(new Search());
            else
                await DisplayAlert("Ви не ввели жодного слова", "Добавте слово", "Ok");
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Verbs());
        }
        private async void button5_Click(object sender, EventArgs e)
        {
            if (App.Database!=null)           
                await Navigation.PushAsync(new OllLibrary());
            else
                await DisplayAlert("В вашому словнику немає слів", "Добавте слово", "Ok");            
        }
    }
}
