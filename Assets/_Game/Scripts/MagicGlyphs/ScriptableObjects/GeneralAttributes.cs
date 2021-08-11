using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs.ScriptableObjects
{
    public class GeneralAttributes : ScriptableObject
    {
        [Header("General")]
        public new string name;
        [TextArea(1, 4)]
        public string description;

        [Header("Attributes")]
        [Range(20, 2000)]
        public float maxLife;

        [Range(1, 30)]
        public float speed;

        //attack area
        public float atkRadius;

        //area to start attacking
        public float radiusDetection;

        public float atkSpeed;
        public float atkDamage;

        public Debuff atkDebuff;


        
        float _maxLife;        
        float _speed;

        //area to start attacking
        float _radiusDetection;

        float _atkSpeed;
        float _atkDamage;

        Debuff _atkDebuff;

        public virtual void Skill()
        {
           // Debug.Log("Classe pai");
        }

        private void OnEnable()
        {
            _maxLife = maxLife;
            _speed = speed;
            _radiusDetection = radiusDetection;
            _atkSpeed = atkSpeed;
            _atkDamage = atkDamage;
            _atkDebuff = atkDebuff;

        }

        public void AddReset()
        {
            MagicGlyphs.Player.PlayerController.deathDelegate += Reset;
        }

        private void OnDisable()
        {
            MagicGlyphs.Player.PlayerController.deathDelegate -= Reset;
        }

        public void Reset()
        {
            maxLife = _maxLife;
            speed = _speed;
            radiusDetection = _radiusDetection;
            atkSpeed = _atkSpeed;
            atkDamage = _atkDamage;
            atkDebuff = _atkDebuff;
        }

    }
}
