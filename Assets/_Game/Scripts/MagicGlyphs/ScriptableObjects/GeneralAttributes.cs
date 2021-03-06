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


     
  
    }
}
