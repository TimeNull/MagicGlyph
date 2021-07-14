using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Characters.Enemies
{
    public abstract class EnemyBehavior : MonoBehaviour //responsable by things that all enemy behaviors must do
    {
        //Here we have animation management, 

        [SerializeField] private Enemy enemy;
        private float atkSpeed;
        private float atkRange;
        private float atkDistance;
        private float atkDamage;
        private AttackType atkType;

        protected virtual void Start()
        {

        }


        protected virtual void Update()
        {

        }

        // Called by Life 
        public void eReceiveMessag(Message message)
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
