using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using DataModel;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.IO;

namespace PhotoClientWPF.ViewModels
{
    public class PhotosViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Photo> photos;

        
        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }

        public ObservableCollection<Photo> Photos
        {
            get
            {
                return photos;
            }
            set
            {
                photos = value;
                OnPropertyChanged("photos");

            }
        }

     

        public PhotosViewModel()
        {
            Photos = PhotoLoader.GetPhoto();
            Add = new DelegateCommand(AddPhoto);
            Remove = new DelegateCommand(DeletePhoto);
        }

        async void AddPhoto(object obj)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                var path = openFileDialog.FileName;
                Photo c = new Photo()
                {
                    AddedDate = DateTime.Now,
                    ClientPhoto = File.ReadAllBytes(path)
                };
                Photos.Add(c);
                await PhotoLoader.AddPhoto(c);
            }
            catch { MessageBox.Show("Oops."); }
        }
   

        void DeletePhoto(object obj)
        {
         //   var selectedPhoto = CollectionViewSource.GetDefaultView(photos).CurrentItem as Photo;
            Photos.Remove(obj as Photo);
            PhotoLoader.DeletePhoto((obj as Photo).Id);
        }
           


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
