using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

[assembly:Dependency(typeof(App2.iOS.FileWorker))]
namespace App2.iOS
{
    class FileWorker : IFileWorker
    {
        public Task<bool> ExistsAsync(string filename)
        {
            string filePath = GetFilePath(filename);
            bool exist = File.Exists(filePath);
            return Task<bool>.FromResult(exist);
        }

        public Task<List<MyLibrary>> LoadTextAsync(string filename)
        {
            List<MyLibrary> temp = new List<MyLibrary>();
            MyLibrary temp1 = new MyLibrary();
            string filePath = GetFilePath(filename);
            using (StreamReader reader = File.OpenText(filePath))
            {
                do
                {
                    temp1.wordLanguage1 = reader.ReadLine();
                    temp1.wordLanguage2 = reader.ReadLine();
                    temp.Add(temp1);
                    temp1 = new MyLibrary();
                }
                while (!reader.EndOfStream);
            }
            return Task.FromResult(temp);
        }

        public Task<bool> SaveText(string filename, List<MyLibrary> MyLib)
        {
            string pathFolder = GetFilePath(filename);
            StreamWriter sw = new StreamWriter(pathFolder);
            foreach (MyLibrary mylib in MyLib)
            {
                sw.WriteLine(mylib.wordLanguage1);
                sw.WriteLine(mylib.wordLanguage2);
            }
            sw.Close();
            return Task.FromResult(true);
        }

        public Task<bool> SaveText(string filename, MyLibrary myiLibrary)
        {
            string pathFolder = GetFilePath(filename);
            StreamWriter sw = new StreamWriter(pathFolder, true);
            sw.WriteLine(myiLibrary.wordLanguage1);
            sw.WriteLine(myiLibrary.wordLanguage2);
            sw.Close();
            return Task.FromResult(true);
        }
        public string GetFilePath(string filename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, filename);
            if (!File.Exists(path))
            {
                if (filename == "1.txt")
                {
                    
                    {
                        File.Copy(filename, path);
                    }
                    
                }
            }
            return path;
        }        
    }
}
