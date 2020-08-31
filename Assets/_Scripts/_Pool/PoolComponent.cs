using System;
using UnityEngine;

namespace PopovMaks.RAV3_Test
{
    public class PoolComponent : MonoBehaviour
    {
        [NonSerialized] 
        public Pool Pool;

        public void Unspawn()
        {
            Pool.Unspawn(gameObject);
        }
    }
}