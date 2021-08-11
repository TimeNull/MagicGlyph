using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MagicGlyphs.Enemies
{
    public class GoblinBehavior : EnemyBehavior //responsable by things that only goblin enemy must do
    {

        // enemyController comes by the base class

        [SerializeField] private float distAtaque = 5, cooldownAttack;
        private bool inAttack, inCooldown;

        protected override void TriggerAttack()
        {
            if (Vector3.Distance(transform.position, enemyController.target.transform.position) <= distAtaque)
            {
                if (!inAttack && !inCooldown)
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

        IEnumerator SetCooldown()
        {
            inCooldown = true;
            yield return new WaitForSeconds(cooldownAttack);
            inCooldown = false;
        }


        public void AttackStartEvent()
        {
            //navMesh.enabled = false;
            inAttack = true;
        }

        public void AttackEndEvent()
        {
            inAttack = false;
            //navMesh.enabled = true;
            StartCoroutine("SetCooldown");
        }

        private void OnDisable()
        {

            navMesh.enabled = true;
            inAttack = false;
            inCooldown = false;

        }


    }
}
