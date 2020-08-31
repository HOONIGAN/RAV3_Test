using UnityEngine;
using System.Collections.Generic;

namespace PopovMaks.RAV3_Test
{
    public static class SaveManager 
    {
        private static readonly IJsonSerializer JsonSerializer = new NewtonsoftSerializer();
        
        public static void SaveInventory(List<ItemData> items)
        {
            Save(ProjectConstants.InventoryPrefsKey, items);
        }

        public static List<ItemData> LoadInventory()
        {
            var items = Load<List<ItemData>>(ProjectConstants.InventoryPrefsKey);
            return items ?? new List<ItemData>();
        }
        
        private static void Save<T>(string key, T obj)
        {
            var json = JsonSerializer.Serialize(obj);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        private static T Load<T>(string key) where T : new()
        {
            string json = string.Empty;
            
            if (PlayerPrefs.HasKey(key))
                json = PlayerPrefs.GetString(key);
            
            T result = JsonSerializer.Deserialize<T>(json);
            return result;
        }
    }
}