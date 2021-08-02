using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MagicGlyphs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Enemy")]
    public class Enemy : GeneralAttributes
    {

        public AttackType attackType;
        


        public override void Skill()
        {
           // Debug.Log("Classe filha hehe");
           // Debug.Log(atkDebuff);
            
        }

    }
}
