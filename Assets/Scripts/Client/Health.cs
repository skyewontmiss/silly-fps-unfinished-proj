using UnityEngine;

namespace Boxfight2.Client.Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float startingHealth = 100f;
        private float currentHealth;

        private void Start()
        {
            currentHealth = startingHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            // Handle death logic here
            Destroy(gameObject);
        }
    }
}
