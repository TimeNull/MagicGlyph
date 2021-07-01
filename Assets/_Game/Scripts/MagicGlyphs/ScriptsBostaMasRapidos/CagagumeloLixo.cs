using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MagicGlyphs
{
    public class CagagumeloLixo : MonoBehaviour
    {
        Animator anim;
        ScriptBostaMobsPegaPlayer getPlayer;
        [SerializeField] float distAtaque = 2;
        [SerializeField] GameObject explosion;
        bool attacking;
        NavMeshAgent navMesh;

        void Start()
        {
            getPlayer = GetComponent<ScriptBostaMobsPegaPlayer>();
            anim = GetComponent<Animator>();
            navMesh = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (getPlayer.jogador)
            {
                if (Vector3.Distance(transform.position, getPlayer.jogador.transform.position) <= distAtaque && !attacking)
                {
                    anim.SetTrigger("Attack");
                }
            }
        }

        public void AttackStartEvent()
        {
            navMesh.enabled = false;
            attacking = true;
        }

        public void AttackEndEvent()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
