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

using System.Windows;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.IO;
using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;

namespace PhotoClientMobile
{
    public class PhotosViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Photo> photos;


        public ICommand Add { get; private set; }
        public ICommand Remove { get; private set; }
        private Photo selectedPhoto;
        private int lastId;

        public Photo SelectedPhoto
        {
            get { return selectedPhoto; }
            set { selectedPhoto = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Photo> Photos
        {
            get
            {
                return photos;
            }
            set
            {
                photos = value;
                lastId = photos[photos.Count - 1].Id;
                OnPropertyChanged("photos");

            }
        }



        public PhotosViewModel()
        {
            Photos = PhotoLoader.GetPhoto();
            Add = new Command(AddPhoto);
            Remove = new Command(DeletePhoto);
        }

        async void AddPhoto(object obj)
        {
          try
            {
                // выбор фото
                var photopath = "";
                Image img = new Image();
                if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                    
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                    photopath = photo.Path;
                                       
                 }



                Photo c = new Photo()
                {
                    Id = ++lastId,
                    AddedDate = DateTime.Now,
                    ClientPhoto = File.ReadAllBytes(photopath)
                };
                Photos.Add(c);
                await PhotoLoader.AddPhoto(c);
            }
            catch { }
        }


        void DeletePhoto(object obj)
        {
            Photos.Remove(obj as Photo);
            PhotoLoader.DeletePhoto(obj as Photo);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
