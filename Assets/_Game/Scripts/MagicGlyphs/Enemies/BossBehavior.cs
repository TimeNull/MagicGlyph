using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Enemies
{

    public class BossBehavior : EnemyBehavior
    {
        // enemyController comes by the base class

        [SerializeField] private float timeToStart = 3f;
        [SerializeField] private float invokeCooldown = 20f;
        [SerializeField] private float attackCooldown = 5f;
        [SerializeField] private float idleCooldown = 2f;
        [SerializeField] private float attackRotationSpeed = 100f;


        private HarmlessPooler enemiesPool = new HarmlessPooler();

        [Header("Mobs Invocados")]
        [SerializeField] private GameObject goblin;
        [SerializeField] private GameObject mushroom;
        [SerializeField] private GameObject tree;

        private Transform mobLocation1;
        private Transform mobLocation2;

        private bool bossIsAlive = true;

        private bool isInvoking = false;
        private bool isAttacking = false;
        private bool isWalking = false;

        private bool isIdle = false; // is boss on idle animation?
        private bool canInvoke = true; // is invoke ready?
        private bool canAttack = true; // is attack ready?


        private void Awake()
        {
            enemiesPool.AddObject(5, goblin);
            enemiesPool.AddObject(3, mushroom);
            enemiesPool.AddObject(2, tree);
            
            
            
            

            enemiesPool.CheckTable();

            mobLocation1 = transform.GetChild(4);
            mobLocation2 = transform.GetChild(5);
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine("BossCycle", timeToStart);
        }
        
        private IEnumerator BossCycle()
        {

            while (bossIsAlive)
            {
                yield return null;

                if (isIdle)
                {
                    Debug.Log("Está em idle");
                    continue;   
                }

                if (!isAttacking && canInvoke)
                {
                    isWalking = false;
                    canInvoke = false;
                    isInvoking = true;
                    InvokeMobs();
                    Debug.Log("Invocou mobs");
                }

                if (enemyController.targetOnRange)
                {
                    if (!isInvoking && canAttack)
                    {
                        isWalking = false;
                        canAttack = false;
                        isAttacking = true;
                        Attack();
                        Debug.Log("Atacou");
                    }
                }

                if (!isWalking && !isInvoking && !isAttacking)
                {
                    isWalking = true;
                    Walk();
                    Debug.Log("Começou a andar");
                }

            }

            yield return null;

        }


        private void Walk()
        {
            anim.SetTrigger(AnimatorNames.BossWalk);          
        }

        private void InvokeMobs()
        {
            if (!bossIsAlive)
                return;

            anim.SetTrigger(AnimatorNames.BossInvoke);
            StartCoroutine(InvokeCooldown());
        }

        private void Attack()
        {
            anim.SetTrigger(AnimatorNames.BossAttack);
            StartCoroutine(AttackCooldown());
        }

        private IEnumerator InvokeCooldown()
        {
            yield return new WaitForSeconds(invokeCooldown);
            canInvoke = true;
        }

        private IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }

        //called automatic by anim event, after invoking or attacking
        public IEnumerator IdleCooldown() //when in idlecooldown boss can do nothing
        {
            Debug.Log("chamou idle");
            yield return new WaitForSeconds(idleCooldown);
            isIdle = false;
            Debug.Log("cabou idle");
        }

        //called by anim event
        public void InvokeStart()
        {
            enemiesPool.GetObject().transform.position = mobLocation1.position;
            enemiesPool.GetObject().transform.position = mobLocation2.position;

        }

        //called by anim event
        public void InvokeEnd()
        {
            
            isIdle = true;
            isInvoking = false;
        }

        //called by anim event
        public void AttackingEnd()
        {
            isAttacking = false;
        }

    }
}
