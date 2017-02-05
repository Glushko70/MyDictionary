using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App2
{
    public partial class Edit : ContentPage
    {
        int number = 0;
        List<MyLibrary> temp=new List<MyLibrary>();
        public Edit()
        {            
            InitializeComponent();
            button1.BackgroundColor = Color.Aqua;
        }
        public Edit(int numb, List<MyLibrary> MyLib)
        {
            Title = "Редагування";
            InitializeComponent();
            number = numb;
            temp = MyLib;                       
            Edit1.Text = temp[numb].wordLanguage1;
            Edit2.Text = temp[numb].wordLanguage2;
            button1.BackgroundColor = Color.Aqua;
        }
        private async void Button1_Click(object sender,EventArgs e)
        {
            temp[number].wordLanguage1 = Edit1.Text;
            temp[number].wordLanguage2 = Edit2.Text;
            await DependencyService.Get<IFileWorker>().SaveText("cardsLibrary", temp);
            await Navigation.PopAsync();
        }
    }
}
