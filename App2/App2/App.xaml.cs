using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App2
{
    public partial class App : Application
    {        
        public const string DATABASE_NAME = "MyLibrary.db";
        public static MyLibraryRepository database;
        public static MyLibraryRepository Database
        {
            get
            {
                if (database == null)
                    database = new MyLibraryRepository(DATABASE_NAME);
                return database;
            }                
        }
        
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {            
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            Card.SaveProperties();
        }

        protected override void OnResume()
        {            
            // Handle when your app OnResume
        }
    }
}
