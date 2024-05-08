using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class ResourceCollector : MonoBehaviour
    {
        public int MoneyCount {  get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ResourceItem resourceItem))
            {
                MoneyCount += resourceItem.ResourceCount;
                Destroy(collision.gameObject);
            }
        }
    }
}