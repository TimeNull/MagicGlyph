using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class AreaDamageScript : MonoBehaviour
    {
        [SerializeField] float radius, tickVelocity, damage;
        [SerializeField] LayerMask layersToAffect;
        Collider[] mobsToDamage;

        private void OnEnable()
        {
            InvokeRepeating("DealDamage", 0f, tickVelocity);
        }


        void DealDamage()
        {
            mobsToDamage = Physics.OverlapSphere(transform.position, radius, layersToAffect);

            if (mobsToDamage.Length > 0)
            {
                foreach(Collider obj in mobsToDamage)
                {
                    obj.gameObject.GetComponent<Life>().ApplyDamage(damage);
                }
            }
        }
    }
}
