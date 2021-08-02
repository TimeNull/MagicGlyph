using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
   

        [SerializeField] protected GeneralAttributes weaponStats;
        [SerializeField] protected LayerMask targetLayer;
        protected float damage;
        protected float attackRadius;
        protected bool m_InAttack;


        protected virtual void Start()
        {
            damage = weaponStats.atkDamage;
            attackRadius = weaponStats.atkRange;
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
