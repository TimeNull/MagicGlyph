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

        private void Update()
        {
            affectedObjects = Physics.OverlapSphere(transform.position, range, layersToAffect);

            foreach (Collider col in affectedObjects)
            {
                Debug.Log(col.transform);
                col.GetComponent<Life>().ApplyDamage(damage);
            }

            if(affectedObjects.Length > 0)
                gameObject.SetActive(false);
        }

    }
}
