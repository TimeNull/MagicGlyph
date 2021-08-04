using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Weapons
{
    public class MeleeWeapon : Weapon
    {

        Collider[] colliders = new Collider[16]; // limiting to 16 enemies at once
        int hitted;

        // Just called the overrides to remember what methods are in the parent class

        public override void BeginAttack()
        {
            base.BeginAttack();
        }

        public override void EndAttack()
        {
            base.EndAttack();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
        }

        protected override void Attack()
        {

            hitted = Physics.OverlapSphereNonAlloc(transform.position, attackRadius, colliders, targetLayer.value); // NonAlloc: it's basically for microperformance

           
            for (int i = 0; i < hitted; i++)
            {
                Life aa = colliders[i].transform.GetComponent<Life>();
                

                if (aa)
                {
                    target = colliders[i].transform.GetComponent<Controller>();
                    aa.ApplyDamage(damage);

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
                    Gizmos.DrawWireSphere(colliders[i].transform.position, attackRadius);

                }
            }
        }

    }
}

