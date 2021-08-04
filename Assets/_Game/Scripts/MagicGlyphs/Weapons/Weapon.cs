using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;
using MagicGlyphs.Enemies;

namespace MagicGlyphs.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private float knockbackForce;
        [SerializeField] private bool uniqueFrameAttack;
        [SerializeField] protected GeneralAttributes weaponStats;
        [SerializeField] protected LayerMask targetLayer;
        protected float damage;
        protected float attackRadius;
        protected bool m_InAttack;
        protected Controller target;


        protected virtual void Start()
        {
            damage = weaponStats.atkDamage;
            attackRadius = weaponStats.atkRadius;
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
                if (uniqueFrameAttack)
                    m_InAttack = false;
                Attack();
                ApplyForce();
            }
        }

        protected virtual void Attack()
        {

        }

        protected virtual void ApplyForce()
        {
            target.AddForce(knockbackForce, transform.forward.normalized);
        }


    }
}
