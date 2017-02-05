
using System;
using System.IO;
using Xamarin.Forms;
using App2.iOS;

[assembly: Dependency(typeof(SQLite_IOS))]

namespace App2.iOS
{
    public partial class SQLite_IOS :IMyLibrary
    {
        public SQLite_IOS() { }
        public string GetDatabasePath(string sgliteFilename)
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, sgliteFilename);
            return path;
        }
    }
}