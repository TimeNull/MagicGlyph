using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Enemies
{
    public class EnemyController : Controller //responsable by things that all enemies must do (animation in general and navmesh)
    {

        [SerializeField] private Enemy enemy;

        public HarmlessPooler whatPoolIBelong;

        protected override void Start()
        {
            base.Start();
            radiusDetection = enemy.radiusDetection;
        }


        //Called by Animation
        public override void AttackBegin()
        {
            base.AttackBegin();

        }

        //Called by Animation
        public override void AttackEnd()
        {
            base.AttackEnd();
        }

        //Called by base.OnReceiveMessage
        protected override void Damaged()
        {
            //animation
        }

        //Called by base.OnReceiveMessage
        protected override void Died()
        {
            //animation
        }

        protected override void OnTargetRange()
        {
            base.OnTargetRange();
            Debug.Log(target.name);
        }

        private void OnDisable()
        {

            whatPoolIBelong?.CheckQueue(); // Debug stuff
            whatPoolIBelong?.FreeObject(gameObject); // Return yourself to the queue when disabled (if this already belongs to one)
            whatPoolIBelong?.CheckQueue(); // Debug stuff
        }
    }
}
