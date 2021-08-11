using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MagicGlyphs
{
    public class AndadaMerdaNavMesh : MonoBehaviour
    {

        NavMeshAgent navMesh;
        ScriptBostaMobsPegaPlayer getPlayer;
        bool executed;

        private void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            getPlayer = GetComponent<ScriptBostaMobsPegaPlayer>();
        }

        private void Update()
        {
            if (getPlayer.jogador)
            {
                navMesh.SetDestination(new Vector3(getPlayer.jogador.position.x, transform.position.y, getPlayer.jogador.position.z));
                executed = true;
            }
            else if (executed)
                navMesh.Move(transform.position);
        }

    }
}
