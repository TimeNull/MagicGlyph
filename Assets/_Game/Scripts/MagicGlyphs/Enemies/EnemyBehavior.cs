using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Enemies
{
    [RequireComponent(typeof(EnemyController))]
    public abstract class EnemyBehavior : MonoBehaviour //responsable by things that all enemy behaviors must do
    {

        protected EnemyController enemyController; //to get ScriptableObject
        protected Animator anim;
        protected NavMeshAgent navMesh;


        private bool executed = false;

        protected virtual void Start()
        {
            enemyController = GetComponent<EnemyController>();
            anim = GetComponent<Animator>();
            navMesh = GetComponent<NavMeshAgent>();
        }

        protected virtual void Update()
        {

            if (enemyController.targetOnRange)
            {
                Debug.Log("chamou");
                Transform targetTransform = enemyController.target.transform;
                if(navMesh.enabled)
                    navMesh.SetDestination(new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z));
                executed = true;
            }
            else if (executed)
            {

                if (navMesh.enabled)
                    navMesh.Move(Vector3.zero);
            }
        }

    }
}
