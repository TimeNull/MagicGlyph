using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;

namespace MagicGlyphs.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Joystick joystick;
        public bool m_skill { get; private set; }

        private bool dirty = false;
         
        private void Start()
        {
            joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
        }

        public void StartSkill() // called by skill buttom, sets m_skill to true by one frame
        {
            if (!dirty)
            {
                m_skill = true;
                dirty = true;
                StartCoroutine(SkillStartEnd());
            }
        }

        private IEnumerator SkillStartEnd() 
        {
            yield return null;
            m_skill = false;
            yield return new WaitForSeconds(3);
            dirty = false;
        }

        public Vector3 Direction()
        {
            return joystick.Direction();
        }

        public float Horizontal()
        {
            return joystick.Horizontal();
        }

        public float Vertical()
        {
            return joystick.Vertical();
        }

    }
}

