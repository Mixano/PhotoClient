using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    
    public class PhotoLoader
    {       
        public static string adress = "http://localhost:5000";
        #region Data
        public static ObservableCollection<Photo> GetPhoto() 
        {
             HttpClient client = new HttpClient();
             client.BaseAddress = new Uri(adress);
             var json = client.GetStringAsync($"/api/photos");
             ObservableCollection<Photo> photos = JsonConvert.DeserializeObject<ObservableCollection<Photo>>(json.Result);
           
             return photos;
        }

        public async static Task<bool> AddPhoto(Photo photo)
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(adress);
            var serializedPhoto = JsonConvert.SerializeObject(photo);
            var response = await client.PostAsync($"/api/photos", new StringContent(serializedPhoto, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public static bool DeletePhoto(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(adress);
            var response = client.DeleteAsync($"/api/photos/{id}").Result;

            return response.IsSuccessStatusCode;
        }
        #endregion
      
    }
}
