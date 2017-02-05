using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;

namespace App2
{
    public partial class AddWord : ContentPage
    {
        Task<List<MyLibrary>> MyLib;
        MyLibrary item = new MyLibrary();
        string filename1=null;
        public AddWord()
        {
            Title = "Добавити слово";
            InitializeComponent();
            button1.BackgroundColor = button2.BackgroundColor = Color.Aqua;                                 
        }
        public AddWord(MyLibrary librari,string filename1)
        {
            Title = "Добавити слово";
            this.filename1 = filename1;
            InitializeComponent();
            button1.BackgroundColor = button2.BackgroundColor = Color.Aqua;
            item.number = librari.number;//Для видалення з загального словника
            Edit1.Text = librari.wordLanguage1;
            Edit2.Text = librari.wordLanguage2;
        }
         
        private  void Button1_Click(object sender, EventArgs e)
        {            
            
            string temp1, temp2;
            temp1 = Edit1.Text;
            temp2 = Edit2.Text;
            if((temp1!=null&&temp1!="")&& (temp2!=null&&temp2!=""))
            {
                int search = Serch1(temp1, "cardsLibrary");
                if (search == -1)//Якщо сово  вже є в словнику
                {
                    int serchverbs = Serch1(temp1, "1.txt");
                    if (serchverbs != -1)
                    {
                        Task<List<MyLibrary>> verbs;
                        verbs = DependencyService.Get<IFileWorker>().LoadTextAsync("1.txt");
                        item.wordLanguage1 = verbs.Result[serchverbs].wordLanguage1;
                        item.wordLanguage2 = verbs.Result[serchverbs].wordLanguage2;
                    }
                    else
                    {
                        item.wordLanguage1 = temp1;
                        item.wordLanguage2 = temp2;
                    }                    
                    DependencyService.Get<IFileWorker>().SaveText("cardsLibrary", item);
                    DisplayAlert("Інформація", "Створена нова карточка", "Ok");
                    if (string.Compare("olllibrary", filename1) == 0)
                        App.Database.DeleteItem(item.number);
                    Edit1.Text = Edit2.Text = null;
                }
                else
                {
                    Edit1.Text = "Таке слово вже є. Карточка номер " + (search + 1);
                    Edit2.Text = null;
                }
            }          
        }
        private async void Button2_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public  int Serch1(String word,string filename)//Пошук слова в словнику повертає номер карточки
        {
            if (DependencyService.Get<IFileWorker>().ExistsAsync(filename).Result)
            {
                MyLib = DependencyService.Get<IFileWorker>().LoadTextAsync(filename);                   
                string compa2 = word.ToLower();
                for (int i = 0; i < MyLib.Result.Count; i++)
                {
                    if (MyLib.Result[i].wordLanguage1 == null)
                        return -1;
                    string compa1 = MyLib.Result[i].wordLanguage1.ToLower();
                    if (Edit1.Text != null)
                        if (compa1.StartsWith(compa2))
                        {
                            return i;
                        }
                }
                
            }           
            return -1;
        }          
    }
}
