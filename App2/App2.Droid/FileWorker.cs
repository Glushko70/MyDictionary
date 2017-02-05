using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(App2.Droid.FileWorker))]

namespace App2.Droid
{
    public class FileWorker : IFileWorker
    {

        public string GetFilePath(string filename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, filename);
            if (!File.Exists(path))
            {
                if (filename == "1.txt")
                {
                    var txtAssetsStream = Forms.Context.Assets.Open(filename);
                    var txtFileStream = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate);
                    var buffer = new byte[1024];
                    int b = buffer.Length;
                    int lenght;
                    while ((lenght = txtAssetsStream.Read(buffer, 0, b)) > 0)
                    {
                        txtFileStream.Write(buffer, 0, lenght);
                    }
                    txtFileStream.Flush();
                    txtFileStream.Close();
                    txtAssetsStream.Close();
                }                
            }
            return path;
        }  

        public  Task<bool> ExistsAsync(string filename)
        {
            string filepath = GetFilePath(filename);//Отримуємо шлях до файла
            bool exists = File.Exists(filepath);//Перевіряємо чи існує файл
            return Task<bool>.FromResult(exists);
        }

        public Task<List<MyLibrary>> LoadTextAsync(string filename)
        {
            List<MyLibrary> temp = new List<MyLibrary>();
            MyLibrary temp1 = new MyLibrary();
            string pathFolder = GetFilePath(filename);
            StreamReader sr = new StreamReader(pathFolder);
            do
            {
                temp1 = new MyLibrary();
                temp1.wordLanguage1 =  sr.ReadLine();
                temp1.wordLanguage2 = sr.ReadLine();
                temp.Add(temp1);
            }
            while (!sr.EndOfStream);
            sr.Close();
            return Task<List<MyLibrary>>.FromResult(temp);
        }

        public Task<bool> SaveText(string filename, List<MyLibrary> MyLib)
        {
            string pathFolder = GetFilePath(filename);
            StreamWriter sw = new StreamWriter(pathFolder);
            for(int i=0;i<MyLib.Count;i++)
            {
                sw.WriteLine(MyLib[i].wordLanguage1);
                sw.WriteLine(MyLib[i].wordLanguage2);
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
    }
}