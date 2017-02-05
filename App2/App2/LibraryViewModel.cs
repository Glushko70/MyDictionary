using System.Collections.ObjectModel;
using System.ComponentModel;


namespace App2
{
    class LibraryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection <MyLibrary> _mylibrary { get; set; }
        public ObservableCollection<MyLibrary> Mylibrary
        {
            set
            {
                _mylibrary = value;
                OnPropertyCahnged("Mylibrary");
            }
            get
            {
                return _mylibrary;
            }
        }

        private void OnPropertyCahnged(string v)
        {
            if(PropertyChanged==null)
                return;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public LibraryViewModel()
        {
            loadText();
        }

        public void UpdateList(ObservableCollection<MyLibrary> list)
        {
            Mylibrary = list;
        }

        public void loadText()
        {
            Mylibrary = new ObservableCollection<MyLibrary> (App.Database.GetItems());
        }
    }
}
