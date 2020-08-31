using UnityEngine;

namespace PopovMaks.RAV3_Test
{
    [CreateAssetMenu(fileName = "New ItemDatabase", menuName = "Inventory/ItemDatabase")]
    public class ItemDatabase : ScriptableObject
    {
        public Item[] items;

        public Item GetItem(int id)
        {
            foreach (var item in items) {
                if (item.id == id)
                    return item;
            }

            return null;
        }
    }
}