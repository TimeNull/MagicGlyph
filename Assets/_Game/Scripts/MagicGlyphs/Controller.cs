using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MagicGlyphs.Weapons;

namespace MagicGlyphs
{
    public abstract class Controller : MonoBehaviour //responsable by things that all controllers must do
    {

        protected Animator anim;

        private GameObject Target;

        private Collider[] targetsOnRange;

        private bool TargetOnRange;

        protected float radiusDetection; // define on child classes 

        [SerializeField] private int maxNumberOfTargets;
        [SerializeField] private LayerMask targetLayer;

        public GameObject target { get => Target;}

        public bool targetOnRange { get => TargetOnRange; }

        [Header("References")]
        protected Weapon weapon;

        protected virtual void Start()
        {
            TargetOnRange = false;
            Target = null;
            anim = GetComponent<Animator>();
            targetsOnRange = new Collider[maxNumberOfTargets];
        }

        protected virtual void Update()
        {
            TargetDetection();
        }

        private void TargetDetection()
        {
            int hitted = Physics.OverlapSphereNonAlloc(transform.position, radiusDetection, targetsOnRange, targetLayer.value);

            if (target)
            {
                OnTargetRange();

                if (Vector3.Distance(Target.transform.position, transform.position) > radiusDetection || !target.activeSelf)
                {
                    Target = null;
                }
            }

            if (!target)
            {   
                GetNewTarget(hitted);
            }

           
        }


        private float minDist = 0;
        private int nearestObject = 0;

        private void GetNewTarget(int numberOfTargets)
        {
            if (numberOfTargets < 1)
            {
                OnTargetNotRange();
                return;
            }
                
                  

            for (int i = 0; i < numberOfTargets; i++)
            {
                if (i == 0)
                {
                    minDist = Vector3.Distance(transform.position, targetsOnRange[i].transform.position);
                    nearestObject = i;
                }
                else if (Vector3.Distance(transform.position, targetsOnRange[i].transform.position) < minDist)
                {
                    minDist = Vector3.Distance(transform.position, targetsOnRange[i].transform.position);
                    nearestObject = i;
                }
            }

            Target = targetsOnRange[nearestObject].gameObject;

            OnTargetRange();
        }

        protected virtual void OnTargetRange()
        {
            TargetOnRange = true;
            // for player controller start attack

        }

        protected virtual void OnTargetNotRange()
        {
            TargetOnRange = false;
            // for player controller finish attack
        }

        // Called by animation
        public virtual void AttackBegin()
        {
            // attack condition and start attack animation (trigger once)
            
        }

        // Called by animation
        public virtual void AttackEnd()
        {
            // attack condition and end attack animation (trigger onde)
        }

        // Called by LifeManager
        public virtual void ReceiveMessage(Message message)
        {
            if (message == Message.DAMAGED)
            {
                Damaged();
            }
            else if (message == Message.DEAD)
            {
                Died();
            }
        }

        // Called by Receive Message
        protected virtual void Damaged()
        {

        }

        // Called by Receive Message
        protected virtual void Died()
        {

        }

    }
}
