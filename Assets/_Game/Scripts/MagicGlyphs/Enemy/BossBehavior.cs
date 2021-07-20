using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.Characters.Enemies;

namespace MagicGlyphs
{
   
    public class BossBehavior : EnemyBehavior
    {
        // enemyController comes by the base class

        [SerializeField] private float timeToStart;

        protected override void Start()
        {
            base.Start();
            StartCoroutine("BossCycle", timeToStart);

        }

        IEnumerator BossCycle()
        {
            //Timer inicial 
            yield return new WaitForSeconds(3f);

            while (true)
            {

            }
        }

        IEnumerator InvokeMobs()
        {
            yield return null;
        }

    }
}
