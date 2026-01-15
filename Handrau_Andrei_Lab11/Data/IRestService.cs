using Handrau_Andrei_Lab11.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Handrau_Andrei_Lab11.Data
{
    public interface IRestService
    {
        Task<List<ShopList>> RefreshDataAsync();
        Task SaveShopListAsync(ShopList item, bool isNewItem);
        Task DeleteShopListAsync(int id);
    }
}
