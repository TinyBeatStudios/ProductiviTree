using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class ShopItemData
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CssFile { get; set; }
    }

    public class ShopDAL
    {
        private static string ConnStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["TestDatabase"].ConnectionString;
            }
        }

        public List<ShopItemData> GetAllShopItems()
        {
            var shopItems = new List<ShopItemData>();
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(
                "SELECT ItemId, Name, Description, Price, CssFile FROM SHOP_ITEMS ORDER BY Price", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shopItems.Add(new ShopItemData
                        {
                            ItemId = (int)reader["ItemId"],
                            Name = (string)reader["Name"],
                            Description = (string)reader["Description"],
                            Price = (int)reader["Price"],
                            CssFile = (string)reader["CssFile"]
                        });
                    }
                }
            }
            return shopItems;
        }

        public List<int> GetOwnedItemIds(int userId)
        {
            var shopItemIds = new List<int>();
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(
                "SELECT ItemId FROM USER_ITEMS WHERE UserId = @UserId", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shopItemIds.Add((int)reader["ItemId"]);
                    }
                }
            }
            return shopItemIds;
        }

        public int GetCurrency(int userId)
        {
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(
                "SELECT Currency FROM USERS WHERE UserId = @UserId", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result == null)
                {
                    return 0;
                } 
                else
                {
                    return (int)result;
                }
            }
        }

        public int? GetEquippedItemId(int userId)
        {
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(
                "SELECT EquippedThemeId FROM USERS WHERE UserId = @UserId", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result == null || result == System.DBNull.Value)
                {
                    return (int?)null;
                }
                else
                {
                    return (int)result;
                }
            }
        }

        public string GetEquippedCssFile(int userId)
        {
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(
                @"SELECT s.CssFile FROM USERS u
                  JOIN SHOP_ITEMS s ON s.ItemId = u.EquippedThemeId
                  WHERE u.UserId = @UserId", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                return cmd.ExecuteScalar() as string;
            }
        }

        public bool TryPurchase(int userId, int ItemId)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    const string deductSql =
                        @"UPDATE u SET u.Currency = u.Currency - s.Price
                          FROM USERS u, SHOP_ITEMS s
                          WHERE u.UserId = @UserId AND s.ItemId = @ItemId
                          AND u.Currency >= s.Price";

                    using (var deduct = new SqlCommand(deductSql, conn, transaction))
                    {
                        deduct.Parameters.AddWithValue("@UserId", userId);
                        deduct.Parameters.AddWithValue("@ItemId", ItemId);
                        
                        if (deduct.ExecuteNonQuery() == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }

                    using (var grant = new SqlCommand(
                        "INSERT INTO USER_ITEMS (UserId, ItemId) VALUES (@UserId, @ItemId)", conn, transaction))
                    {
                        grant.Parameters.AddWithValue("@UserId", userId);
                        grant.Parameters.AddWithValue("@ItemId", ItemId);
                        grant.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
            }
        }

        public bool Equip(int userId, int itemId)
        {
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(
                @"UPDATE USERS SET EquippedThemeId = @ItemId
                  WHERE UserId = @UserId
                    AND (EXISTS (SELECT 1 FROM USER_ITEMS WHERE UserId = @UserId AND ItemId = @ItemId)
                        OR (SELECT Price FROM SHOP_ITEMS WHERE ItemId = @ItemId) = 0)", conn))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
