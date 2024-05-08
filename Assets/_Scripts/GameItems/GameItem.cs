using Common.Managers;
using UnityEngine;

namespace Mechanics
{
    public class GameItem : MonoBehaviour
    {
        private const float BOUND_OFFSET = 1;

        private ItemRespawnManager _itemRespawnManager;

        protected virtual void Awake()
        {
            _itemRespawnManager = ItemRespawnManager.Instance;
        }

        private void FixedUpdate()
        {
            if (transform.position.y < -_itemRespawnManager.CameraTopY - BOUND_OFFSET || transform.position.x < -_itemRespawnManager.CameraRight - BOUND_OFFSET 
                || transform.position.x > _itemRespawnManager.CameraRight + BOUND_OFFSET)
            {
                Destroy(gameObject);
            }
        }
    }
}