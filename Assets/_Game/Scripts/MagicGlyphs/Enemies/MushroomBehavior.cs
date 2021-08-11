using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Enemies
{
    public class MushroomBehavior : EnemyBehavior //responsable by things that only mushroom enemy must do
    {
        [SerializeField] private float distAtaque = 5;
        [SerializeField] GameObject explosion;
        private bool inAttack;


        // enemyController comes by the base class


        protected override void TriggerAttack()
        {
            if (Vector3.Distance(transform.position, enemyController.target.transform.position) <= distAtaque)
            {
                if (!inAttack)
                    anim.SetTrigger("Attack");

                if (navMesh.enabled)
                    navMesh.enabled = false;

            }
            else if (!inAttack)
            {
                if (!navMesh.enabled)
                    navMesh.enabled = true;
            }
        }


        public void AttackStartEvent()
        {
            inAttack = true;
        }

        public void AttackEndEvent()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            inAttack = false;
            navMesh.enabled = true;
        }
    }

}
