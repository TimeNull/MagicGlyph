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
        [SerializeField] private SkinnedMeshRenderer playerMesh;
        [SerializeField] private Material damageMaterial;

        private Material mainMaterial;

        private PlayerInput playerInput;
        private CharacterController cc;

        // ------------------- PLAYER PARAMETERS ----------------------


        const float gravity = -9.84f;
        const float groundedRayDistance = 1f;

        private Vector3 velocity; //just for x and z
        private Vector2 checkVelocity;

        // ------------------- BUFFERS ----------------------------

        // Prevents animation triggers from being called 17 times in a row 

        private bool m_move = false;
        private bool m_attack = false;
        private bool m_skill = false;
       // private bool m_switch = false;
        private bool _frameAttack;




        [SerializeField] private float rotationSpeed;

        

        protected override void Start()
        {
            base.Start();
            

            //------ Components inicialization ------

            cc = GetComponent<CharacterController>();

            anim = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();


            weapon = GetComponentInChildren<Weapon>();
            _frameAttack = weapon.frameAttack;

            //------ SO inicialization ------
            UpdateStats();

            mainMaterial = playerMesh.material;
           

        }

        protected override void Update()
        {
            base.Update();

            Move();
        }

        private void FixedUpdate()
        {

            if (playerInput.m_skill)
            {
                m_skill = true;
                Skill();
            }
        }

        protected override void OnTargetRange()
        {
            if (m_skill)
                return;

            base.OnTargetRange();

            
            Utility.rotateTowards(transform, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), 5f);
            //transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
           
            if (!m_attack)
            {
                TriggerAttack(true);
                m_attack = true;
            }
            
        }

        protected override void OnTargetNotRange()
        {
       
            base.OnTargetNotRange();

            if (m_attack)
            {
                TriggerAttack(false);
                m_attack = false;
            }
        }

        //ehhh.. move
        private void Move()
        {
            cc.Move(velocity * Time.deltaTime);

            if (m_skill)
                return;
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

                //if (targetOnRange)
                //{
                //    m_switch = true;
                //}

                if (checkVelocity.magnitude > 0.1f && !m_move && !m_skill)
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

            

        }

        public override void ReceiveMessage(Message message)
        {
            base.ReceiveMessage(message);

        }

        //Called by Target
        public void TriggerAttack(bool state)
        {
            anim.SetBool(AnimatorNames.PlayerAttack, state);
        }


        protected override void Damaged()
        {
            base.Damaged();
            //animation and feedback stuff here
            StartCoroutine(ChangeMaterial());

           // Debug.Log("animação de levou dano");

        }

        IEnumerator ChangeMaterial()
        {
            playerMesh.material = damageMaterial;
            yield return new WaitForSeconds(0.1f);
            playerMesh.material = mainMaterial;
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
            m_skill = true;
            anim.SetTrigger(AnimatorNames.PlayerSkill);
        }

        
        public void SkillStart()
        {
            m_skill = true;
            weapon.SkillDamage(1);
            if (anim.GetBool(AnimatorNames.PlayerAttack))
                anim.SetBool(AnimatorNames.PlayerAttack, false);

            _frameAttack = weapon.frameAttack;
            if (weapon.frameAttack)
                weapon.frameAttack = false;
        }

        public void SkillEnd()
        {
            m_attack = false;
            if (weapon.frameAttack != _frameAttack)
                weapon.frameAttack = _frameAttack;
            m_skill = false;
            m_move = false;
        }

    }

}
