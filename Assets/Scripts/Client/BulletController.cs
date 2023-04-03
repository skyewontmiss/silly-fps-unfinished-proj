using UnityEngine;
using Boxfight2.Client.Player;

namespace Boxfight2.Client.Weapons
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private float damage = 1f;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }

        public void SetDamage(float damage)
        {
            this.damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("touched gameobject " + other.name);

            var health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("touched gameobject " + other.gameObject.name);
            var health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
