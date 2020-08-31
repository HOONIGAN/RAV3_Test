using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PopovMaks.RAV3_Test
{
    public class InventorySlot : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image _icon;

        private Item _item;
        
        public bool PointerIsOverObject { get; private set; }

        private void OnDisable()
        {
            PointerIsOverObject = false;
        }

        public void AddItem(Item newItem)
        {
            _item = newItem;
            _icon.color = _item.color;
        }

        public void ClearSlot()
        {
            _item = null;
            _icon.color = Color.grey;
        }

        private void RemoveItem()
        {
            Debug.Log($"[InventorySlot] Drop item: {_item.name}");
            Inventory.instance.Remove(_item);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (_item == null)
                return;
            RemoveItem();
            PointerIsOverObject = false;
        }

        public void OnPointerEnter(PointerEventData eventData) {
            eventData.pointerPress = this.gameObject;
            PointerIsOverObject = _item != null;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerIsOverObject = false;
        }
    }
}