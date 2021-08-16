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
            UpdateStats();  
        }

        public void UpdateStats()
        {
            speed = enemy.speed;
            radiusDetection = enemy.radiusDetection;
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
          //  Debug.Log(target.name);
        }

        public void GivePlayerMoney()
        {
            CoinsManager.addCoins(Random.Range(1, 7));
        }

        protected override void OnDisable()
        {
            base.OnDisable();
          //  whatPoolIBelong?.CheckQueue(); // Debug stuff
            whatPoolIBelong?.FreeObject(gameObject); // Return yourself to the queue when disabled (if this already belongs to one)
          //  whatPoolIBelong?.CheckQueue(); // Debug stuff


        }
    }
}
