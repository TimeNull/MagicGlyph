using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.Weapons
{
    public class Weapon : MonoBehaviour
    {
        GameObject m_Owner;

        //avoid self harm
        public void SetOwner(GameObject owner)
        {
            m_Owner = owner;
        }
    }
}
