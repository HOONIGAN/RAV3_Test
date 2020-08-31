using System.Collections;
using UnityEngine;

namespace PopovMaks.RAV3_Test
{
    public class GameApp : MonoBehaviour, IAsyncProcessor
    {
        private GameClientApi _apiClient;

        private void Awake()
        {
            _apiClient = new GameClientApi(this, new UnitySerializer());
            
            DontDestroyOnLoad(this);
        }

        public void StartRoutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public void OnInventoryItemAdd(Item item)
        {
            _apiClient.UpdateItemStatus(new ItemStatusUpdateRequest
            {
                id = item.id,
                name = item.name,
                weight = item.weight,
                eventName = ProjectConstants.InventoryItemAddEvent
            }, () => Debug.Log($"Success"), error => Debug.LogError($"Error: {error}"));
        }

        public void OnInventoryItemRemove(Item item)
        {
            _apiClient.UpdateItemStatus(new ItemStatusUpdateRequest
            {
                id = item.id,
                name = item.name,
                weight = item.weight,
                eventName = ProjectConstants.InventoryItemRemoveEvent
            }, () => Debug.Log($"Success"), error => Debug.LogError($"Error: {error}"));
        }
    }
}
