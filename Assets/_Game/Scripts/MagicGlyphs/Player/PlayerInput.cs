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

        private void Start()
        {
            joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
        }

        public void StartSkill()
        {
            m_skill = true;
            StartCoroutine(SkillStartEnd());
        }

        private IEnumerator SkillStartEnd() // called by skill buttom, sets m_skill to true by one frame
        {
            yield return null;
            m_skill = false;
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

