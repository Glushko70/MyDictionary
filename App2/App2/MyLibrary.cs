using SQLite;

namespace App2
{
    [Table("MyLibrary")]
    public class MyLibrary        
    {
        [PrimaryKey, AutoIncrement, Column("number")]
        public int number { get; set; }
        public string wordLanguage1 { get; set; }
        public string wordLanguage2 { get; set; }
    }
}
