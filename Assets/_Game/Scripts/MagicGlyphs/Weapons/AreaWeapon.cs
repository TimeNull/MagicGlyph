using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.Weapons;

namespace MagicGlyphs
{
    public class AreaWeapon : Weapon
    {

        [SerializeField] private float maxDistance;

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

            Ray ray = new Ray(transform.position, transform.forward);

            hitted = Physics.SphereCastNonAlloc(ray, attackRadius, raycastHits, maxDistance, targetLayer.value); // NonAlloc: it's basically for microperformance


            for (int i = 0; i < hitted; i++)
            {
                Life aa = raycastHits[i].transform.GetComponent<Life>();

               

                if (aa)
                {
                    target = raycastHits[i].transform.GetComponent<Controller>();
                    aa.ApplyDamage(damage);

                }

            }
        }

    }

}
