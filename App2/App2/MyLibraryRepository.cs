using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using System;

namespace App2
{
    public class MyLibraryRepository
    {
        SQLiteConnection database;
        public MyLibraryRepository(string filename)//Конструктор з параметрами для створення бази даних
        {
            string databasePath = DependencyService.Get<IMyLibrary>().GetDatabasePath(filename);
            database = new SQLiteConnection(databasePath);
            database.CreateTable<MyLibrary>();//Створити базу даних
        }
        public List <MyLibrary> GetItems()
        {            
            return (from i in database.Table<MyLibrary>() select i).ToList();//Повертає всі обєкти
        }
        public MyLibrary GetItems(int number)//Повертає 1 обєкт за номером
        {
            try
            {
                return database.Get<MyLibrary>(number);
            }
            catch 
            {
                return null;
            }
        }
        public int DeleteItem(int number)//Видалення щбєкта за номером
        {            
            return database.Delete<MyLibrary>(number);            
        }
        public int SaveItem(MyLibrary item)//Добавити обєкт
        {
            if (item.number != 0)//Якщо вже є перезаписуємо обєкт з ідентифікатором namber
            {
                database.Update(item);
                return item.number;
            }
            else//Якщо немає добавляємо обєкт
                return database.Insert(item);                    
        }       
    }
}
