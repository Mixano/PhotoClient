using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataModel
{
    public class Photo : INotifyPropertyChanged
    {

        int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }


        public DateTime AddedDate { get; set; }

        byte[] photo;
        public byte[] ClientPhoto
        {
            get
            { return photo; }
            set
            {
                photo = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
