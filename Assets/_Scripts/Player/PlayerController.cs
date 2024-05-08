using System;
using UnityEngine;

namespace Mechanics.Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnDamage;
        public event Action OnHeal;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out FoodItem foodItem))
            {
                OnHeal?.Invoke();
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.TryGetComponent(out EnemyItem enemyItem))
            {
                OnDamage?.Invoke();
                Destroy(collision.gameObject);
            }
        }
    }
}