using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PopovMaks.RAV3_Test
{
    public class InventoryUI : MonoBehaviour 
    {
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private InventorySlot _slotPrefab;
        
        private Inventory _inventory;
        private readonly List<InventorySlot> _slots = new List<InventorySlot>();
        
        public bool PointerIsOverSlot => _slots.Any(s => s.PointerIsOverObject);

        private void Start () {
            // dependence
            _inventory = GetComponentInParent<Inventory>();
            
            _inventory.ItemRemoveEvent.AddListener(OnItemRemoveHandler);
        }

        public void Open()
        {
            if (_slotPrefab == null)
            {
                Debug.LogError("[InventoryUI] Slot prefab is null!");
                return;
            }
            
            gameObject.SetActive(true);
            
            for (int i = 0; i < _inventory.Space; i++)
            {
                var slot = PoolManager.Spawn(_slotPrefab.gameObject).GetComponent<InventorySlot>();
                slot.transform.SetParent(_slotsContainer);
                slot.transform.localScale = Vector3.one;
                
                _slots.Add(slot);
                
                if (i < _inventory.items.Count)
                    slot.AddItem(_inventory.items[i]);
                else
                    slot.ClearSlot();
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
            
            foreach (var s in _slots)
            {
                PoolManager.Unspawn(s.gameObject);
            }

            _slots.Clear();
        }

        private void OnItemRemoveHandler(Item item)
        {
            Close();
        }
    }
}