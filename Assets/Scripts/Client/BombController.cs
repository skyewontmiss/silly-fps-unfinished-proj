using UnityEngine;
using Boxfight2.Physics;
using System.Collections;
using System.Collections.Generic;

namespace Boxfight2.Client.Weapons
{
    public class BombController : MonoBehaviour
    {
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private float blastRadius = 5f;
        [SerializeField] private float explosionForce = 700f;
        [SerializeField] private float time = 700f;
        [SerializeField] private Inventory inv;
        [SerializeField] private MonoBehaviour behavior;
        [SerializeField] private GameObject explosionEffect;
        [SerializeField] private GameObject player;


        private bool _hasExploded = false;
        /*[HideInInspector]*/ public bool CanBeHeld;
        /*[HideInInspector]*/ public bool IsBeingThrown;
        /*[HideInInspector]*/ public int amountOfThrows;
        Rigidbody _rb;
        Transform initialParent;
        private void Awake()
        {
            initialParent = transform.parent;
            CanBeHeld = false;
            IsBeingThrown = false;
            amountOfThrows = 0;
            _hasExploded = false;
        }

        IEnumerator ExplodePls()
        {
            yield return new WaitForSeconds(time);

            if (_hasExploded)
            {
                yield break;
            }

            _hasExploded = true;
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            explosion.name = "Explosion FX";

            Destroy(explosion, 2f);
            if (GetComponent<Rigidbody>() != null)
                Destroy(GetComponent<Rigidbody>());

            transform.parent = initialParent;
            transform.localPosition = Vector3.zero;
            behavior.enabled = true;
            if (amountOfThrows > 0)
                CanBeHeld = true;

            IsBeingThrown = false;
            inv.HideBomb();
        }


        public void ThrowBomb(Vector3 lookDirection)
        {
            if (amountOfThrows > 0)
            {
                amountOfThrows--;
            }
                CanBeHeld = false;
            IsBeingThrown = true;
            _hasExploded = false;
            if (GetComponent<Rigidbody>() == null)
                _rb = gameObject.AddComponent<Rigidbody>();


            // Calculate the initial velocity based on the look direction and throw force
            Vector3 initialVelocity = lookDirection * throwForce;
            _rb.velocity = initialVelocity;

            // Set the bomb's transform to be just in front of the player
            Vector3 offset = lookDirection * 0.5f; // Adjust this value as needed
            Vector3 bombPosition = transform.localPosition + offset;
            transform.localPosition = bombPosition;
            _rb.AddForce(lookDirection * throwForce, ForceMode.Impulse);
            StartCoroutine(ExplodePls());
            transform.parent = null;
        }
    }
}