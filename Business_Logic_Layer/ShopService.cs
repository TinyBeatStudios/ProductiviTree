using System;
using System.Collections.Generic;
using System.Linq;
using Data_Access_Layer;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public enum PurchaseResult
    {
        Success,
        NotEnoughMoney,
        AlreadyOwned,
        ItemNotFound
    }

    public class ShopItemView
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string CssFile { get; set; }
        public bool Owned { get; set; }
        public bool Equipped { get; set; }
    }

    public class ShopService
    {
        private const string DefaultTheme = "default.css";
        private readonly ShopDAL _shopDAL = new ShopDAL();

        public List<ShopItemView> GetShopItems(int userId)
        {
            var owned = _shopDAL.GetOwnedItemIds(userId);
            int? equipped = _shopDAL.GetEquippedItemId(userId);

            return _shopDAL.GetAllShopItems().Select(i => new ShopItemView
            {
                ItemId = i.ItemId,
                Name = i.Name,
                Description = i.Description,
                Price = (int)i.Price,
                CssFile = i.CssFile,
                Owned = i.Price == 0 || owned.Contains(i.ItemId),
                Equipped = equipped == i.ItemId
            }).ToList();
        }

        public int GetCurrency(int userId)
        {
            return _shopDAL.GetCurrency(userId);
        }

        public PurchaseResult Purchase(int userId, int itemId)
        {
            var item = _shopDAL.GetAllShopItems().FirstOrDefault(i => i.ItemId == itemId);
            if (item == null)
            {
                return PurchaseResult.ItemNotFound;
            }
                
            if (item.Price == 0 || _shopDAL.GetOwnedItemIds(userId).Contains(itemId))
            {
                return PurchaseResult.AlreadyOwned;
            }
                
            if (_shopDAL.GetCurrency(userId) < item.Price)
            {
                return PurchaseResult.NotEnoughMoney;
            }
                
            return _shopDAL.TryPurchase(userId, itemId) ? PurchaseResult.Success : PurchaseResult.NotEnoughMoney;
        }

        public bool Equip(int userId, int itemId)
        {
            return _shopDAL.Equip(userId, itemId);
        }

        public string GetThemeCssFile(int userId)
        {
            return _shopDAL.GetEquippedCssFile(userId) ?? DefaultTheme; 
        }


    }

}
