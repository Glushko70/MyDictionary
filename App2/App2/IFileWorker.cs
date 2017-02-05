using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface IFileWorker
    {
        Task<bool> ExistsAsync(string filename);//Перевірка на існування файла
        Task<List<MyLibrary>> LoadTextAsync(string filename);//Читання з файла       
        Task<bool> SaveText(string filename, MyLibrary myiLibrary);
        Task<bool> SaveText(string filename, List<MyLibrary> MyLib);  
              
    }
}
