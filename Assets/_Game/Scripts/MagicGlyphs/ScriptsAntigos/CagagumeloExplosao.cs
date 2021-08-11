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
            InvokeRepeating("TickDamage", 0, 1);
        }

        private void TickDamage()
        {
            DealDamage(tickDamage);
        }

        public void Disapear()
        {
            StartCoroutine("DisapearTime");
        }

        IEnumerator DisapearTime()
        {
            yield return new WaitForSeconds(toxicCloudDuration);
            CancelInvoke("TickDamage");
            gameObject.SetActive(false);
        }
    }
}
