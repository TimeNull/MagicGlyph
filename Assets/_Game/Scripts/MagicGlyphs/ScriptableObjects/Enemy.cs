using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MagicGlyphs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
    public class Enemy : ScriptableObject
    {
        [Header("General")]
        public new string name;
        [TextArea(1, 4)]
        public string description;

        [Header("Attributes")]
        [Range(20, 200)]
        public float maxLife;
        [Range(5, 50)]
        public float speed;
        //alcançe do ataque em si
        public float atkRange;
        //área para começar a atacar
        public float atkDistance;
        public float atkSpeed;
        public float atkDamage;
        public bool isStatic;

        public AttackType attackType;

        public Debuff atkDebuff;


    }
}
