using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.Weapons;

namespace MagicGlyphs
{
    public class AreaWeapon : Weapon
    {
        [SerializeField] private float attackRadius;
        [SerializeField] private float maxDistance;
        [SerializeField] private float damage;
        [SerializeField] private LayerMask enemyLayer;

        RaycastHit[] raycastHits = new RaycastHit[16]; // limiting to 16 enemies at once
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
            m_InAttack = false;

            Ray ray = new Ray(transform.position, transform.forward);

            hitted = Physics.SphereCastNonAlloc(ray, attackRadius, raycastHits, maxDistance, enemyLayer.value); // NonAlloc: it's basically for microperformance


            for (int i = 0; i < hitted; i++)
            {
                Life aa = raycastHits[i].transform.GetComponent<Life>();

                if (aa)
                {

                    aa.ApplyDamage(damage);

                }

            }
        }

    }

}
