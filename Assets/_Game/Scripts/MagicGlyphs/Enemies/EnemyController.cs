using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Enemies
{
    public class EnemyController : Controller //responsable by things that all enemies must do (animation in general and navmesh)
    {

        [SerializeField] private Enemy enemy;
        [SerializeField] private Material damageMaterial;

        private Material mainMaterial;
        private MeshRenderer enemyMesh;

        public HarmlessPooler whatPoolIBelong;

        private void Awake()
        {
            UpdateStats();
            enemyMesh = GetComponent<MeshRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            mainMaterial = enemyMesh.material;
        }

        public void UpdateStats()
        {
            speed = enemy.speed;
            radiusDetection = enemy.radiusDetection;
        }

        //Called by base.OnReceiveMessage
        protected override void Damaged()
        {
            base.Damaged();
            //animation and feedback stuff here
            StartCoroutine(ChangeMaterial());

        }

        IEnumerator ChangeMaterial()
        {
            enemyMesh.material = damageMaterial;
            yield return new WaitForSeconds(0.1f);
            enemyMesh.material = mainMaterial;
        }

        //Called by base.OnReceiveMessage
        protected override void Died()
        {
            //animation
            //calling the enemies left method need to be here, because the unityevent on life script does not support taking via inspector objects from other scenes
            //also if I want I could delay the Disable of this object due to animation stuff, so I can't put this on disable too, because would delay the actual checking
            PassLevelManage.CheckEnemies();
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
