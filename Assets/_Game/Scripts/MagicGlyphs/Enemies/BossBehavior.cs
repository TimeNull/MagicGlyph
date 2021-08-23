using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Enemies
{

    public class BossBehavior : EnemyBehavior
    {
        // enemyController comes by the base class

        [SerializeField] private float timeToStart = 3f;
        [SerializeField] private float invokeCooldown = 10f;
        [SerializeField] private float attackCooldown = 15f;
        [SerializeField] private float idleCooldown = 2f;
        [SerializeField] private float rotationSpeed = 1.5f;

        private Transform player;

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

        private bool hasExitedRange = false;
        private bool animFlag = false;


        protected override void Initialize()
        {
            player = GameObject.FindWithTag("Player").transform;

            // enemiesPool.CheckTable();

            mobLocation1 = transform.GetChild(4);
            mobLocation2 = transform.GetChild(5);
        }

        private void Start()
        {
            enemiesPool.AddObject(2, goblin);
            enemiesPool.AddObject(1, mushroom);
            enemiesPool.AddObject(2, goblin);
            enemiesPool.AddObject(1, tree);
            enemiesPool.AddObject(2, goblin);
            enemiesPool.AddObject(2, mushroom);
            enemiesPool.AddObject(2, tree);

            StartCoroutine("BossCycle", timeToStart);
        }

        private void OnEnable()
        {
            Player.PlayerController.deathDelegate += StopBoss;
        }

        private void OnDisable()
        {
            Player.PlayerController.deathDelegate -= StopBoss;
        }

        private IEnumerator BossCycle()
        {
            while (!player)
            {
                player = GameObject.FindWithTag("Player").transform;
                yield return null;
            }

            while (bossIsAlive)
            {
                yield return null;

                if (isIdle)
                {
                   // Debug.Log("Está em idle");
                    continue;   
                }

                if (!isAttacking && canInvoke)
                {
                    isWalking = false;
                    canInvoke = false;
                    isInvoking = true;
                    InvokeMobs();
                   // Debug.Log("Invocou mobs");
                }

                if (enemyController.targetOnRange)
                {
                    if (!isInvoking && canAttack)
                    {
                        isWalking = false;
                        canAttack = false;
                        isAttacking = true;
                        AttackStart();
                       // Debug.Log("Atacou");
                    }
                }

                if (!isWalking && !isInvoking && !isAttacking)
                {
                    isWalking = true;
                    Walk();
                  //  Debug.Log("Começou a andar");
                }

            }

            yield return null;

        }

        protected override void Update()
        {
            if (!isWalking)
            {
                // transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                Utility.rotateTowards(transform, player.position, rotationSpeed);
                
            }

            if (isWalking)
            {
                if (enemyController.targetOnRange && !animFlag)
                {
                    anim.SetTrigger(AnimatorNames.BossIdle);
                    hasExitedRange = true;
                    animFlag = true;
                }

                if (!enemyController.targetOnRange && hasExitedRange && animFlag)
                {
                    anim.SetTrigger(AnimatorNames.BossWalk);
                    animFlag = false;
                }
                
                if (navMesh.enabled)
                    navMesh.SetDestination(new Vector3(player.position.x, transform.position.y, player.position.z));
            }
        }

        private void Walk()
        {
            animFlag = false;
            SetNavMeshState(true);

            anim.SetTrigger(AnimatorNames.BossWalk);          
        }


        private void InvokeMobs()
        {
            ResetTriggers(4);

            SetNavMeshState(false);

            if (!bossIsAlive)
                return;

            anim.SetTrigger(AnimatorNames.BossInvoke);
            StartCoroutine(InvokeCooldown());
        }

        private void AttackStart()
        {
            ResetTriggers(1);

            SetNavMeshState(false);

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
           // Debug.Log("chamou idle");
            yield return new WaitForSeconds(idleCooldown);
            isIdle = false;
           // Debug.Log("cabou idle");
        }

        //called by anim event
        public void InvokeStart()
        {
            PassLevelManage.enemiesQtde += 2;
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
            isIdle = true;
            isAttacking = false;
        }

        private void SetNavMeshState(bool state)
        {
            if(navMesh.enabled != state)
                navMesh.enabled = state;
        }

        private void ResetTriggers(int except)
        {
            if(except != 1)
                anim.ResetTrigger(AnimatorNames.BossAttack);
                
            if (except != 2)
                anim.ResetTrigger(AnimatorNames.BossIdle);
                
            if (except != 3)
                anim.ResetTrigger(AnimatorNames.BossWalk);
               
            if (except != 4)
                anim.ResetTrigger(AnimatorNames.BossInvoke);
                
        }

        public void StopBoss()
        {
            bossIsAlive = false;
        }
    }
}
