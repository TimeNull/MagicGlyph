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
        
        [Header("References")]
        protected Weapon weapon;


        private GameObject Target;
        public GameObject target { get => Target; set => Target = value; }

        public virtual void TriggerAttack()
        {
            //attack animation
            anim.SetTrigger("attack");
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
