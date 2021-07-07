using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class ArvureTristeBolota : MonoBehaviour
    {
        Rigidbody rb;
        public Transform player;
        public float maxHeight = 8;
        Vector3 vel;

        [SerializeField] float damage, range;
        [SerializeField] LayerMask layersToAffect;
        Collider[] affectedObjects;


        private void Start()
        {
            maxHeight += transform.position.y;

            rb = GetComponent<Rigidbody>();
            vel.x = (player.position.x - transform.position.x) / 
                    (Mathf.Sqrt(2 * maxHeight / 9.81f) + Mathf.Sqrt (2 * (maxHeight - (player.position.y - transform.position.y)) / 9.81f));

            vel.z = (player.position.z - transform.position.z) /
                    (Mathf.Sqrt(2 * maxHeight / 9.81f) + Mathf.Sqrt(2 * (maxHeight - (player.position.y - transform.position.y)) / 9.81f));

            vel.y = Mathf.Sqrt(2 * 9.81f * maxHeight);


            rb.velocity = vel;


        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Life>())
                other.gameObject.GetComponent<Life>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }

    }
}
