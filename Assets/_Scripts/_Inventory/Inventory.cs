using System.Collections.Generic;
using UnityEngine;

namespace PopovMaks.RAV3_Test    
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private float _dropOffset;
        [SerializeField] private ItemDatabase _itemDatabase;
        [SerializeField] private InventoryUI _inventoryUI;
        
        public static Inventory instance;

        public InventoryItemAddEvent ItemAddEvent;
        public InventoryItemRemoveEvent ItemRemoveEvent;
        
        public readonly List<Item> items = new List<Item>();
        
        public int Space => ProjectConstants.InventorySlotCount;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("[Inventory] More than one instance of Inventory found!");
                return;
            }

            instance = this;
        }

        private void Start() 
        {
            var savedItems = SaveManager.LoadInventory();
            foreach (var itemData in savedItems) 
                items.Add(_itemDatabase.GetItem(itemData.id));
            
            _inventoryUI.Close();
        }

        private void OnApplicationPause(bool pauseStatus) 
        {
            if (pauseStatus)
                SaveItems();
        }
        
        private void OnApplicationQuit()
        {
            SaveItems();
        }

        private void SaveItems()
        {
            List<ItemData> itemsToSave = new List<ItemData>();
            foreach (var item in items) 
                itemsToSave.Add(new ItemData {id = item.id});
            SaveManager.SaveInventory(itemsToSave);
        }

        public bool Add(Item item)
        {
            if (items.Count >= Space)
            {
                Debug.Log("[Inventory] Not enough room.");
                return false;
            }

            items.Add(item);

            ItemAddEvent.Invoke(item);

            return true;
        }
        
        public void Remove(Item item)
        {
            var droppedItem = PoolManager.Spawn(_itemDatabase.GetItem(item.id).prefab);
            droppedItem.transform.position = transform.position + transform.right * _dropOffset;
            
            items.Remove(item);

            ItemRemoveEvent.Invoke(item);
        }

        public void OnMouseDown()
        {
            _inventoryUI.Open();
        }

        public void OnMouseUp() 
        {
            // bad solution
            if (!_inventoryUI.PointerIsOverSlot)
                _inventoryUI.Close();
        }
    }
}