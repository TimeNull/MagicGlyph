using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Enemies
{
    public class TreeBehavior : EnemyBehavior //responsable by things that only tree enemy must do
    {
        bool inCoolDown;
        [SerializeField] GameObject granade;
        [SerializeField] float coolDownTime;

        protected override void TriggerAttack()
        {
            if (!inCoolDown)
            {
                StartCoroutine(ThrowGranade());
                GameObject a = Instantiate(granade, transform.position, Quaternion.identity);
                a.GetComponent<ArvureTristeBolota>().player = enemyController.target.transform;
            }

        }

        IEnumerator ThrowGranade()
        {
            inCoolDown = true;
            yield return new WaitForSeconds(coolDownTime);
            inCoolDown = false;
        }

        private void OnDisable()
        {
            navMesh.enabled = true;
        }

    }
}

