using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs.Characters.Enemies
{
    [RequireComponent(typeof(EnemyController))]
    public abstract class EnemyBehavior : MonoBehaviour //responsable by things that all enemy behaviors must do
    {

        protected EnemyController enemyController; //to get ScriptableObject

        protected virtual void Start()
        {
            enemyController = GetComponent<EnemyController>();
        }

    }
}
