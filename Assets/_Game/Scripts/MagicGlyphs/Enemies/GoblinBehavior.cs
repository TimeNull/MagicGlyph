using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MagicGlyphs.Enemies
{
    public class GoblinBehavior : EnemyBehavior //responsable by things that only goblin enemy must do
    {

        // enemyController comes by the base class

        [SerializeField] float distAtaque = 5, cooldownAttack;
        bool inAttack, onCooldown;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (enemyController.targetOnRange)
                TriggerAttack();
        }



        private void TriggerAttack()
        {
            if (Vector3.Distance(transform.position, enemyController.target.transform.position) <= distAtaque && !inAttack && !onCooldown)
            {
                anim.SetTrigger("Attack");
            }
        }

        IEnumerator SetCooldown()
        {
            onCooldown = true;
            yield return new WaitForSeconds(cooldownAttack);
            onCooldown = false;
        }

        public void AttackStartEvent()
        {
            navMesh.enabled = false;
            inAttack = true;
        }

        public void AttackEndEvent()
        {
            inAttack = false;
            navMesh.enabled = true;
            StartCoroutine("SetCooldown");
        }

    }
}
