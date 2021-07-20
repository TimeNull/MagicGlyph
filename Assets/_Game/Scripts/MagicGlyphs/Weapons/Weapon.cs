using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Weapons
{
    public class Weapon : MonoBehaviour
    {
   

        [SerializeField] protected GeneralAttributes weaponStats;

        protected bool m_InAttack;


        private void Start()
        {
            
        }


        public virtual void BeginAttack()
        {
            m_InAttack = true;
        }

        public virtual void EndAttack()
        {
            m_InAttack = false;
        }

        protected virtual void FixedUpdate()
        {
            if (m_InAttack)
            {
                Attack();
            }
        }

        protected virtual void Attack()
        {

        }


    }
}
