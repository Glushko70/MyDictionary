using System;
using System.Collections.Generic;
using Windows.Storage;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Xamarin.Forms;

[assembly: Dependency(typeof(App2.UWP.FileWorker))]
namespace App2.UWP
{
    class FileWorker : IFileWorker
    {
        public  Task<bool> ExistsAsync(string filename)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            string path = localFolder.Path;
            string filePath = Path.Combine(path, filename);
            bool exist = File.Exists(filePath);
            return Task<bool>.FromResult(exist);
        }

        public async Task<List<MyLibrary>> LoadTextAsync(string filename)
        {            
            string filePath = null;
            List<MyLibrary> temp = new List<MyLibrary>();
            MyLibrary temp1 = new MyLibrary();
            if (await ExistsAsync(filename) == false && filename == "1.txt")
                copyTXT(filename);            
            StorageFolder localFolder1 = ApplicationData.Current.LocalFolder;
            filePath = Path.Combine(localFolder1.Path, filename);            
            int f = 2;
            foreach(string temp2 in File.ReadAllLines(filePath))
            {
                if (f % 2 == 0)
                {
                    temp1.wordLanguage1 = temp2;
                    f++;
                }
                else
                {
                    temp1.wordLanguage2 = temp2;
                    f++;
                    temp.Add(temp1);
                    temp1 = new MyLibrary();
                }
            }
                                     
            return temp;
        }

        private void copyTXT(string filename)
        {
            StorageFolder fileTXT = Package.Current.InstalledLocation;
            var str = fileTXT.Path+@"\Assets"+ "\\1.txt";
            var filePath = ApplicationData.Current.LocalFolder;
            string pathTXT = Path.Combine(filePath.Path, filename);
            File.Copy(str, pathTXT);        
        }

        public async Task<bool> SaveText(string filename, List<MyLibrary> MyLib)
        {
            IList<string> temp2 = new List<string>();
            for (int i = 0; i < MyLib.Count ; i++)
            {                
                temp2.Add(MyLib[i].wordLanguage1);
                temp2.Add(MyLib[i].wordLanguage2);
            }
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile library = await localFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteLinesAsync(library, temp2);
            return true;
        }

        public async Task<bool> SaveText(string filename, MyLibrary myiLibrary)
        {
            var lf = ApplicationData.Current.LocalFolder;
            var filePath = Path.Combine(lf.Path, filename);
            using (StreamWriter sw = File.AppendText(filePath))
            {
                await sw.WriteLineAsync(myiLibrary.wordLanguage1);
                await sw.WriteLineAsync(myiLibrary.wordLanguage2);
            }
            return true;
        }
    }
}