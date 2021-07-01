using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Weapons
{
    public class MeleeWeapon : Weapon
    {
        private bool m_InAttack;

        [SerializeField] private float attackRadius;
        [SerializeField] private LayerMask layer;

        Collider[] colliders = new Collider[10];
        int hitted;

        public void BeginAttack()
        {
            m_InAttack = true;
        }

        public void EndAttack()
        {
            m_InAttack = false;
        }

        private void FixedUpdate()
        {
            if (m_InAttack)
            {
                
                hitted = Physics.OverlapSphereNonAlloc(transform.position, attackRadius, colliders, layer); // NonAlloc: it is for user use his own array, and limit the number of collisions

                for (int i = 0; i < hitted; i++)
                {
                    Life aa = colliders[i].transform.GetComponent<Life>();
                    if (aa && aa.transform.gameObject.layer == layer)
                        aa.ApplyDamage(200f);

                }
            }
        }

        private void OnDrawGizmos()
        {
            if (m_InAttack)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, attackRadius);


                for (int i = 0; i < hitted; i++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(colliders[i].transform.position, attackRadius/ 1.5f);

                }
            }
        }

    }
}

