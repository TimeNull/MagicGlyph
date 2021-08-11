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

        protected EnemyController enemyController; 
        protected Animator anim;
        protected NavMeshAgent navMesh;

        private bool executed = false;

        protected void Awake()
        {
            enemyController = GetComponent<EnemyController>();
            anim = GetComponent<Animator>();
            navMesh = GetComponent<NavMeshAgent>();
            Initialize();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void Update()
        {

            FollowTarget();
            EnableForce();

            if (enemyController.targetOnRange)
                TriggerAttack();
  
        }

        protected virtual void FollowTarget()
        {
            if (enemyController.targetOnRange)
            {
                Transform targetTransform = enemyController.target.transform;
                if (navMesh.enabled)
                    navMesh.SetDestination(new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z));
                executed = true;
            }
            else if (executed)
            {

                if (navMesh.enabled)
                    navMesh.Move(Vector3.zero);
            }
        }

        private void EnableForce()
        {
            if (enemyController.underForce)
            {
                if (navMesh.enabled)
                    navMesh.enabled = false;
            }
            else if (!enemyController.underForce)
            {
                if (!navMesh.enabled)
                    navMesh.enabled = true;
            }
        }

        protected virtual void TriggerAttack()
        {

        }


    }
}
