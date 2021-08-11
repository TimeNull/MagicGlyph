using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;

namespace MagicGlyphs.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private Joystick joystick;
        private bool m_skill;
        [SerializeField] private float m_skill_wait;
        public bool Skill { get => m_skill; }

        private void Start()
        {
            joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
        }


        private void Update()
        {
            if (true) // condition to 
            {
                StartCoroutine(SkillWait());
            }
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


        IEnumerator SkillWait()
        {
            m_skill = true;
            yield return m_skill_wait;
            m_skill = false;
        }


    }
}

