using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Weapons
{
    public class Weapon : MonoBehaviour
    {
        GameObject m_Owner;

        protected bool m_InAttack;

        //avoid self harm
        public void SetOwner(GameObject owner)
        {
            m_Owner = owner;
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
