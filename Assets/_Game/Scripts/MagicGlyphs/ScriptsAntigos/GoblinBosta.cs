using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MagicGlyphs
{
    public class GoblinBosta : MonoBehaviour
    {
        Animator anim;
        ScriptBostaMobsPegaPlayer getPlayer;
        [SerializeField] float distAtaque = 5, cooldownAttack;
        bool inAttack, onCooldown;
        NavMeshAgent navMesh;

        // Start is called before the first frame update
        void Start()
        {
            getPlayer = GetComponent<ScriptBostaMobsPegaPlayer>();
            anim = GetComponent<Animator>();
            navMesh = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (getPlayer.jogador)
            {
                if (Vector3.Distance(transform.position, getPlayer.jogador.transform.position) <= distAtaque && !inAttack && !onCooldown)
                {
                    anim.SetTrigger("Attack");
                }
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
