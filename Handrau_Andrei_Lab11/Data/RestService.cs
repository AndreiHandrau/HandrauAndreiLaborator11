using Handrau_Andrei_Lab11.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handrau_Andrei_Lab11.Data
{
    public class RestService : IRestService
    {
        HttpClient client;

        string RestUrl = "https://192.168.1.136:45455/api/shoplists/{0}";

        public List<ShopList> Items { get; private set; }

        public RestService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (msg, cert, chain, errors) => true;

            client = new HttpClient(handler);
        }

        public async Task<List<ShopList>> RefreshDataAsync()
        {
            Items = new List<ShopList>();
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<ShopList>>(content);
                }
            }
            catch { }

            return Items;
        }

        public async Task SaveShopListAsync(ShopList item, bool isNewItem)
        {
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));
            string json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            if (isNewItem)
                await client.PostAsync(uri, content);
            else
                await client.PutAsync(uri, content);
        }

        public async Task DeleteShopListAsync(int id)
        {
            Uri uri = new Uri(string.Format(RestUrl, id));
            await client.DeleteAsync(uri);
        }
    }
}
