using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MagicGlyphs.Weapons;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs
{
    public abstract class Controller : MonoBehaviour //responsable by things that all controllers must do
    {

        protected Animator anim;
        private GameObject Target;
        private Collider[] targetsOnRange;

        

        private bool UnderForce;
        public bool underForce { get => UnderForce; }

        protected float force;
        protected Vector3 direction;

        private bool TargetOnRange;

        public float speed { get; set; }
        protected float radiusDetection; // define on child classes 

        [SerializeField] private int maxNumberOfTargets;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private bool applyForce;

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
            weapon = GetComponentInChildren<Weapon>();
        }

        protected virtual void Update()
        {
            TargetDetection();


            if (underForce && applyForce)
            {
                //Debug.Log("adicionando força");
                AddingForce();
            }
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

        public virtual void OnHit()
        {

        }

        // Called by animation
        public virtual void AttackBegin()
        {
            // attack condition and start attack animation (trigger once)
            weapon.BeginAttack();
            
        }

        // Called by animation
        public virtual void AttackEnd()
        {
            // attack condition and end attack animation (trigger onde)
            weapon.EndAttack();
        }

        public void AddForce(float force, Vector3 attackingDirection)
        {
            if (!underForce)
            {
                UnderForce = true;
                StartCoroutine(ForceCooldown());
            }

            this.force = force;
            this.direction = attackingDirection;

        }

        IEnumerator ForceCooldown()
        {
            yield return new WaitForSeconds(0.5f);
            UnderForce = false;
        }


        protected virtual void AddingForce()
        {

            Vector3 a = Vector3.zero;
            transform.position = Vector3.Lerp(transform.position, transform.position + (direction * force), Time.deltaTime);
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

        protected virtual void OnDisable()
        {
            force = 0;
            direction = Vector3.zero;
            UnderForce = false;
        }

    }
}
