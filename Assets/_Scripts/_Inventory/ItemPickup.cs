using UnityEngine;

namespace PopovMaks.RAV3_Test
{
    [RequireComponent(typeof(Rigidbody))]
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private float _dragSpeed = 10f;

        private Rigidbody _rb;

        private Vector3 _objectOffset;
        private float _objectScreenPosZ;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (item == null)
            {
                Debug.LogError("[ItemPickup] ItemData is null!");
                return;
            }
            _rb.mass = item.weight;
        }

        private void OnMouseDown()
        {
            _objectScreenPosZ = Camera.main.WorldToScreenPoint(transform.position).z;
            _objectOffset = transform.position - GetMouseWorldPos();

            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void OnMouseDrag()
        {
            _rb.velocity = ((GetMouseWorldPos() + _objectOffset) - transform.position) * _dragSpeed;
        }

        private void OnMouseUp()
        {
            _rb.constraints = RigidbodyConstraints.None;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(ProjectConstants.BackpackTag))
                {
                    bool success = Inventory.instance.Add(item);
                    if (success)
                        PoolManager.Unspawn(gameObject);
                }
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _objectScreenPosZ;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}