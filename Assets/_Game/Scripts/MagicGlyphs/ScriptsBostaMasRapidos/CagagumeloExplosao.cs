using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class CagagumeloExplosao : MonoBehaviour
    {
        [SerializeField] float explosionRay, toxicCloudDuration, explosionDamage, tickDamage;
        [SerializeField] LayerMask layersToAffect;
        Collider[] affectedObjects;
        bool callTickDamage;

        private void Update()
        {
            if (callTickDamage) {
                StartCoroutine("TickDamage");
            }
        }

        void DealDamage(float damage)
        {
            affectedObjects =  Physics.OverlapSphere(transform.position, explosionRay, layersToAffect);

            foreach(Collider col in affectedObjects)
            {
                col.GetComponent<Life>().ApplyDamage(damage);
            }
        }

        public void ExplosionActions()
        {
            DealDamage(explosionDamage);
        }

        IEnumerator TickDamage()
        {
            callTickDamage = false;
            yield return new WaitForSeconds(1);
            callTickDamage = true;
            DealDamage(tickDamage);
        }

        public void Disapear()
        {
            StartCoroutine("DisapearTime");
        }

        IEnumerator DisapearTime()
        {
            yield return new WaitForSeconds(toxicCloudDuration);
            gameObject.SetActive(false);
        }
    }
}
