using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Characters.Enemies
{
    public class EnemyController : Controller //responsable by things that all enemies must do
    {

        [SerializeField] private Enemy enemy;

        public override void TriggerAttack()
        {
            base.TriggerAttack();

            //setdestination 
        }

        public override void AttackBegin()
        {
            base.AttackBegin();

        }

        public override void AttackEnd()
        {
            base.AttackEnd();
        }

    }
}
