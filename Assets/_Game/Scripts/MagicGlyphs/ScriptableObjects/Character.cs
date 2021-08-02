using System.Collections.Generic;
using UnityEngine;


namespace MagicGlyphs.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]
    public class Character : GeneralAttributes
    {

        public Skill[] skills = new Skill[1];

    }
}
