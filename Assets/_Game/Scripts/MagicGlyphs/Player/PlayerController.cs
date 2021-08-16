using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;
using MagicGlyphs.Weapons;

public delegate void NewDelegate();

namespace MagicGlyphs.Player
{
    [RequireComponent(typeof(PlayerInput))] //project script
    [RequireComponent(typeof(CharacterController))] //unity component
    [RequireComponent(typeof(Animator))] //unity component

    public class PlayerController : Controller //responsable by things that all characters must do
    {
        public static NewDelegate deathDelegate, updateStats;

        // ------------------- REFERENCES ----------------------------

        [SerializeField] private Character character;

        private PlayerInput playerInput;
        private CharacterController cc;

        // ------------------- PLAYER PARAMETERS ----------------------


        const float gravity = -9.84f;
        const float groundedRayDistance = 1f;

        private Vector3 velocity; //just for x and z
        private Vector2 checkVelocity;

        // ------------------- BUFFERS ----------------------------

        // Prevents animation triggers from being called 17 times in a row 

        private bool m_move;
        private bool m_attack = false;




        [SerializeField] private float rotationSpeed;

        

        protected override void Start()
        {
            base.Start();

           // DontDestroyOnLoad(gameObject);

            //------ Components inicialization ------

            cc = GetComponent<CharacterController>();

            anim = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();


            weapon = GetComponentInChildren<Weapon>();

            //------ SO inicialization ------
            UpdateStats();
           

        }

        protected override void Update()
        {
            base.Update();

            Move();
        }

        private void FixedUpdate()
        {

            if (playerInput.Skill)
            {
                Skill();
            }
        }

        protected override void OnTargetRange()
        {
            
            base.OnTargetRange();

            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
           
            if (!m_attack)
            {
                TriggerAttack();
                m_attack = true;
            }
            
        }

        protected override void OnTargetNotRange()
        {
       
            base.OnTargetNotRange();

            if (m_attack)
            {
                TriggerAttack();
                m_attack = false;
            }
        }

        //ehhh.. move
        private void Move()
        {
            
            cc.Move(playerInput.Direction() * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            if(cc.isGrounded && velocity.y < 0)
               velocity.y = -2f;
            


            checkVelocity.x = cc.velocity.x;
            checkVelocity.y = cc.velocity.z;

            if (checkVelocity.magnitude > 0)
            {
                if (!targetOnRange)
                {
                    //look at own transform direction while moving
                    Quaternion toRotation = Quaternion.LookRotation(playerInput.Direction(), Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
                }

                if (checkVelocity.magnitude > 0.1f && !m_move)
                {
                    m_move = true;
                    anim.SetTrigger(AnimatorNames.PlayerRun);
                }
            }
            else
            {
                if (m_move)
                {
                    m_move = false;
                    anim.SetTrigger(AnimatorNames.PlayerIdle);
                }
            }

            cc.Move(velocity * Time.deltaTime);

        }

        public override void ReceiveMessage(Message message)
        {
            base.ReceiveMessage(message);

        }

        //Called by Target
        public void TriggerAttack()
        {
            anim.SetTrigger(AnimatorNames.PlayerAttack);
        }


        protected override void Damaged()
        {
            base.Damaged();
            //animation and feedback stuff here
           // Debug.Log("animação de levou dano");

        }

        protected override void Died()
        {
            base.Died();
            deathDelegate?.Invoke();
            //animation and feedback stuff here
          //  Debug.Log("animação de morreu");
        }

        //Called by SOLoader
        public void UpdateStats()
        {
            speed = character.speed;
            radiusDetection = character.radiusDetection;
        }

        void Skill()
        {

        }


        private void OnTriggerEnter(Collider other)
        {

        }
    }

}
