using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Boxfight2.Physics
{
    [RequireComponent(typeof(LineRenderer))]
    public class Explosion : MonoBehaviour
    {
        private LineRenderer renderer;

        [Header("Explosion Tuning")]
        [SerializeField] private int DefferedPoints;
        [SerializeField] private float MaxRadius;
        [SerializeField] private float Speed;
        [SerializeField] private float StartWidth;
        [SerializeField] private float KnockbackForce;
        void Awake()
        {
            renderer = GetComponent<LineRenderer>();
            renderer.positionCount = DefferedPoints + 1;
            StartCoroutine(Blast());
        }

        IEnumerator Blast()
        {
            float currentRadius = 0f;

            while (currentRadius < MaxRadius)
            {
                currentRadius += Time.deltaTime * Speed;
                Draw(currentRadius);
                Damage(currentRadius);
                yield return null;
            }
        }

        void Damage(float radius)
        {
            Collider[] colliders = UnityEngine.Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(direction * KnockbackForce, ForceMode.Impulse);
                }

                Destructible destructible = nearbyObject.GetComponent<Destructible>();
                if (destructible != null)
                {
                    destructible.DestroyMe();
                }
            }
        }

        private void Draw(float radius)
        {
            float angle = 360f / DefferedPoints;

            for (int i = 0; i <= DefferedPoints; i++)
            {
                float a = i * angle * Mathf.Deg2Rad;
                Vector3 dir = new Vector3(Mathf.Sin(a), Mathf.Cos(a), 0f);
                Vector3 position = dir * radius;

                if (i < renderer.positionCount) // check if index is within bounds
                {
                    renderer.SetPosition(i, position);
                }
            }

            renderer.widthMultiplier = Mathf.Lerp(0f, StartWidth, 1f - radius / MaxRadius);
        }

    }

}