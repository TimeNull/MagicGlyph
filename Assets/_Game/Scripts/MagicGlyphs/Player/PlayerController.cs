using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;
using MagicGlyphs.Weapons;

namespace MagicGlyphs.Characters
{
    [RequireComponent(typeof(PlayerInput))] //project script
    [RequireComponent(typeof(CharacterController))] //unity component
    [RequireComponent(typeof(Animator))] //unity component

    public class PlayerController : Controller //responsable by things that all characters must do
    {
        // ------------------- REFERENCES ----------------------------

        [SerializeField] private Character character;
        [SerializeField] private Character holder;


        private PlayerInput playerInput;
        private CharacterController cc;

        // ------------------- PLAYER PARAMETERS ----------------------

        private float speed;
        private float atkDistance;
        private float atkSpeed;
        private float atkDamage;
        const float gravity = -9.84f;
        const float groundedRayDistance = 1f;
        private Vector3 velocity; //just for x and z
        private Vector2 checkVelocity;

        // ------------------- BUFFERS ----------------------------

        // Prevents animation triggers from being called 17 times in a row 

        private bool m_move;
        private bool m_attack;




        [SerializeField] private float rotationSpeed;


        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            //------ Components inicialization ------

            cc = GetComponent<CharacterController>();

            anim = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();

            weapon = GetComponentInChildren<Weapon>(); 

            //------ SO inicialization ------

            speed = character.startSpeed;
            atkDamage = character.atkDamage;
            atkDistance = character.atkDistance;
            atkSpeed = character.atkSpeed;

            holder.startSpeed = character.startSpeed;
            holder.atkDamage = character.atkDamage;
            holder.atkDistance = atkDistance;
            holder.atkSpeed = character.atkSpeed;



        }

        private void Update()
        {
            // for our playtests and balancing

            speed = holder.startSpeed;
            atkDamage = holder.atkDamage;
            atkDistance = holder.atkDistance;
            atkSpeed = holder.atkSpeed;

            Move();
        }


        private void FixedUpdate()
        {

            if (playerInput.Skill)
            {
                Skill();
            }
        }

        //ehhh.. move
        void Move()
        {
            
            cc.Move(playerInput.Direction() * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            if(cc.isGrounded && velocity.y < 0)
               velocity.y = -2f;
            


            checkVelocity.x = cc.velocity.x;
            checkVelocity.y = cc.velocity.z;

            if (checkVelocity.magnitude > 0)
            {
                if (!GetComponent<Target>().nearestGameObject)
                {
                    //look at own transform direction while moving
                    Quaternion toRotation = Quaternion.LookRotation(playerInput.Direction(), Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
                }

                if (checkVelocity.magnitude > 0.1f && !m_move)
                {
                    m_move = true;
                    anim.SetTrigger("run");
                }
            }
            else
            {
                if (m_move)
                {
                    m_move = false;
                    anim.SetTrigger("idle");
                }
            }

            cc.Move(velocity * Time.deltaTime);

        }

        public override void ReceiveMessage(Message message)
        {
            base.ReceiveMessage(message);

        }

        //Called by animation
        public override void AttackBegin()
        {
            base.AttackBegin();
            weapon.BeginAttack();

        }

        //Called by animation
        public override void AttackEnd()
        {
            base.AttackEnd();
            weapon.EndAttack();

        }

        protected override void Damaged()
        {
            base.Damaged();
            //animation and feedback stuff here
            Debug.Log("animação de levou dano");

        }

        protected override void Died()
        {
            base.Died();
            //animation and feedback stuff here
            Debug.Log("animação de morreu");
        }


        void Skill()
        {

        }

        // Called by animation event
        void MeleeAttack()
        {

        }

        void GetMana(bool got, int value)
        {
            // gold/mana system features here
        }


        private void OnTriggerEnter(Collider other)
        {

        }
    }

}
