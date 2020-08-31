using UnityEngine;

namespace PopovMaks.RAV3_Test
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string name;
        public float weight;

        [Space(10f)]
        [Tooltip("Color as item icon")]
        public Color color;
        public GameObject prefab;
    }
}