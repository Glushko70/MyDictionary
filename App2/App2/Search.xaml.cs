using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace App2
{
    public partial class Search : ContentPage
    {
        List<MyLibrary> MyLib=new List<MyLibrary>();
        int number = -1;        
        public Search()
        {
            Title = "Пошук";           
            InitializeComponent();
            loadText();
            button1.BackgroundColor = button2.BackgroundColor = button4.BackgroundColor = Color.Aqua;
            button3.BackgroundColor = Color.Aqua;
        }
        /// <summary>
        /// Загрузка всіх карточок
        /// </summary>
        public async void loadText()
        {
            MyLib = await DependencyService.Get<IFileWorker>().LoadTextAsync("cardsLibrary");
        }
        /// <summary>
        /// Пошук слова в карточках
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {                      
            for (int i=0;i< MyLib.Count;i++)
            {
                if (Edit1.Text != null)//Перевірка чи введене слово для пошуку інакше нічого не відбувається
                {
                    string compa2 = Edit1.Text.ToLower();
                    string compa1 = MyLib[i].wordLanguage1.ToLower();
                    if(compa1.StartsWith(compa2))
                    {
                        Label1.Text = "Карточка " + (i + 1).ToString();
                        Label2.Text = MyLib[i].wordLanguage1;
                        Label3.Text = MyLib[i].wordLanguage2;
                        number = i;
                    }
                }                                              
            }                    
        }
        /// <summary>
        /// Перехід на сторінку редагування карточки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button2_Click(object sender,EventArgs e)
        {
            Label1.Text = Label2.Text = Label3.Text = Edit1.Text = null;            
            if (number!=-1)
                await Navigation.PushAsync(new Edit(number,MyLib)); //Передача номера карточки на сторінку редагування           
        }        
        private async void Button4_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (number != -1)
            {
                MyLib.RemoveAt(number);
                DependencyService.Get<IFileWorker>().SaveText("cardsLibrary", MyLib);
                DisplayAlert("Інформація", "Слово видалене", "Ok");
                Label1.Text = Label2.Text = Label3.Text = null;
                Edit1.Text = "";
            }

           
        }
    }
}
