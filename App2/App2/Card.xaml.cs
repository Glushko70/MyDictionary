using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace App2
{
    public partial class Card : ContentPage
    {
        static int numb = -1,change=1,i=0; //Лічильник карточок, вибір мови, переключатель перекладу    
        int maxnumb=0;
        List<MyLibrary> temp = new List<MyLibrary>();
        public Card()
        {
            Title = "Карточки";           
            InitializeComponent();
            loadText();            
            maxnumb = temp.Count;
            startPage();
            button1.BackgroundColor = button4.BackgroundColor = Color.Aqua;
            button3.BackgroundColor = button2.BackgroundColor = button6.BackgroundColor = Color.Aqua;
            button5.BackgroundColor = Color.Yellow;
            button5.TextColor = Color.Blue;          
        }
        public async void loadText()
        {
            temp = await DependencyService.Get<IFileWorker>().LoadTextAsync("cardsLibrary");
        }
        private async void Button1_Click(object sender, EventArgs e)
        {
            SaveProperties();
            await Navigation.PopAsync();
        }
        private  void Button2_Click(object sender, EventArgs e)
        {            
            numb++;
            if (numb >= 0 && numb < maxnumb)
            {                
                label3.Text = "Карточка номер " + (numb+1).ToString()+" з "+(maxnumb).ToString();
                if (change == 1)
                {
                    label1.Text = temp[numb].wordLanguage1;
                    i = 0;
                }
                else
                {
                    label1.Text = temp[numb].wordLanguage2;
                    i = 0;
                }
            }                                   
            else
            {
                numb = maxnumb;            
                label1.Text = "Карточки закінчилися";
                label3.Text = null;                
            }
            label2.Text = null;
        }
        private  void Button3_Click(object sender, EventArgs e)
        {
            numb--;
            if (numb >=0 && numb < maxnumb)
            {
                label3.Text = "Карточка номер " + (numb+1).ToString() + " з " + (maxnumb).ToString();
                i = 0;                          
                if (change == 1)
                    label1.Text = temp[numb].wordLanguage1;
                else
                    label1.Text = temp[numb].wordLanguage2;               
            }
            else
            {
                numb = -1;
                label1.Text = "Карточки закінчилися";
                label3.Text = null;
            }
            label2.Text = null;
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            if (numb >= 0 && numb < (maxnumb))
            {
                if (i == 0)
                {
                    i++;
                    if (change == 1)
                        label2.Text = temp[numb].wordLanguage2;
                    else
                        label2.Text = temp[numb].wordLanguage1;
                }
                else
                {
                    label2.Text = null;
                    i--;
                }
            }
            else
            {
                label2.Text = "Добавте карточку";
                label3.Text = null;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            
            if (numb >= 0 && numb < (maxnumb))
            {                
                label2.Text = null;
                if (change == 1)
                {
                    button5.Text = "Українська-Англійська";
                    label1.Text = temp[numb].wordLanguage2;
                    i = 0;
                    change = 0;
                }
                else
                {
                    button5.Text = "English - Ukraine";
                    label1.Text = temp[numb].wordLanguage1;
                    i = 0;
                    change = 1;
                }
            }
        }
        private async void Button6_Click(object sender, EventArgs e)
        {
            MyLibrary data = new MyLibrary();
            if (numb >= 0 && numb < maxnumb)
            {
                data.wordLanguage1 = temp[numb].wordLanguage1;
                data.wordLanguage2 = temp[numb].wordLanguage2;
                App.Database.SaveItem(data);
                temp.RemoveAt(numb);
                if (await DependencyService.Get<IFileWorker>().SaveText("cardsLibrary", temp))
                    await DisplayAlert("Інформація", "Слово перенесене в словник", "Ok");
                maxnumb--;
                try
                {
                    label1.Text = temp[numb].wordLanguage1;
                    label2.Text = null;
                    label3.Text = "Карточка номер " + (numb + 1).ToString() + " з " + (maxnumb).ToString();
                }
                catch
                {
                    label1.Text = "Карточки закінчилися";
                    label2.Text = label3.Text = null;
                }
            }
            else
                await DisplayAlert("Інформація", "Виберіть карточку", "Ok");
        }
        public void startPage()
        {                             
            object number = "",Change="";           
            if (App.Current.Properties.TryGetValue("number",out number))
            {
                numb = (int)number;
                if(numb >= maxnumb)
                    numb = maxnumb-1;
                if (numb < 0)
                    numb = 0;
                if (App.Current.Properties.TryGetValue("Change", out Change))
                    change = (int)Change;               
                switch (change)
                {
                    case 0:                     
                        label1.Text = temp[numb].wordLanguage2;
                        label3.Text = "Карточка номер " + (numb+1).ToString() + " з " + (maxnumb).ToString();
                        button5.Text = "Українська-Англійська";                      
                        break;
                    case 1:                       
                        label1.Text = temp[numb].wordLanguage1;
                        label3.Text = "Карточка номер " + (numb+1).ToString() + " з " + (maxnumb).ToString();                        
                        break;
                    default:
                        break;
                }
                label2.Text = null;
            }
            else
            {                             
                label2.Text = null;
                if (numb>= 0&&numb<maxnumb)
                {
                    label1.Text = temp[numb].wordLanguage1;
                    label3.Text = "Карточка номер " + (numb + 1).ToString() + " з " + (maxnumb).ToString();
                    change = 1;                    
                }
                else
                {
                    numb = 0;
                    label1.Text = temp[numb].wordLanguage1;
                    label3.Text = "Карточка номер " + (numb + 1).ToString() + " з " + (maxnumb).ToString();
                }                
            }
        }
        public static void SaveProperties()
        {
            int value1 = change;
            int value = numb;
            App.Current.Properties["number"] = value;
            App.Current.Properties["Change"] = value1;
        }       
    }
}
