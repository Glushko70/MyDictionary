using System;
using System.IO;
using Xamarin.Forms;
using App2.Droid;

[assembly: Dependency(typeof(SQLite_Android))]
namespace App2.Droid
{
    class SQLite_Android:IMyLibrary
    {
        public SQLite_Android() { }
        public string GetDatabasePath(string sqlitefilename)
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, sqlitefilename);
            return path;
        }
    }
}