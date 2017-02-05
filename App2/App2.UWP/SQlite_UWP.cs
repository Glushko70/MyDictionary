using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using App2.UWP;

[assembly: Dependency(typeof(SQlite_UWP))]

namespace App2.UWP
{
    class SQlite_UWP: IMyLibrary
    {
        public SQlite_UWP() { }
        public string GetDatabasePath (string sqliteFilename)
        {
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return path;
        }
    }
}
 