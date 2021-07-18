using System.Collections.Generic;
using UnityEngine;


namespace MagicGlyphs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]
    public class Character : ScriptableObject
    {
        [Header("General")]
        public new string name;
        [TextArea(1, 4)]
        public string description;

        [Header("Attributes")]
        [Range(20, 2000)]
        public float maxLife;
        [Range(20, 2000)]
        public float startMana;
        [Range(1, 30)]
        public float startSpeed;
        //área para começar a atacar
        public float atkDistance;
        public float atkSpeed;
        public float atkDamage;

        public Skill[] skills = new Skill[1];

    }
}
