using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float knockbackForce;
        [SerializeField] private bool uniqueFrameAttack;
        
        public bool frameAttack { get { return uniqueFrameAttack; } set { uniqueFrameAttack = value;  } } //done by player skill only

        [SerializeField] protected GeneralAttributes weaponStats;
        [SerializeField] protected LayerMask targetLayer;
        protected float damage;
        protected float attackRadius;
        protected bool m_InAttack;
        protected Controller[] target;
        protected bool m_aplyingForce;
        protected bool m_underSkill;


        protected virtual void Start()
        {
            damage = weaponStats.atkDamage;
            attackRadius = weaponStats.atkRadius;
            target = new Controller[5];
        }


        public virtual void BeginAttack()
        {
            m_InAttack = true;
        }

        public virtual void EndAttack()
        {
            m_InAttack = false;
            m_aplyingForce = false;
            if (m_underSkill)
            {
                damage = weaponStats.atkDamage;
                m_underSkill = false;
            }
        }

        protected virtual void FixedUpdate()
        {
            if (m_InAttack)
            {
                if (uniqueFrameAttack)
                    m_InAttack = false;
                Attack();

                if (!m_aplyingForce)
                {
                    m_aplyingForce = true;
                    ApplyForce();
                }
                    
            }
        }

        public void SkillDamage(float value)
        {
            m_underSkill = true;
            damage = value;
        }

        public virtual void AmplifyAttack(float quantity)
        {
            damage += quantity;
        }

        protected virtual void Attack()
        {

        }

        protected virtual void ApplyForce()
        {

        }
    }
}
