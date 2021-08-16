using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicGlyphs.ScriptableObjects;

namespace MagicGlyphs
{
    public abstract class Buffs : MonoBehaviour
    {
        [SerializeField] protected GameObject player;
    }

    public class BuffItens : Buffs
    {
        [SerializeField] amplifyType amplify;
        public float increaseAmount;

        private void OnEnable()
        {
            switch (amplify)
            {
                case amplifyType.attack:
                    player.GetComponent<MagicGlyphs.Weapons.Weapon>().AmplifyAttack(increaseAmount);
                    break;
                case amplifyType.life:
                    player.GetComponent<Life>().SetMaxLife(player.GetComponent<Life>().MaxLife + increaseAmount);
                    player.GetComponent<Life>().IncreaseActualLife(increaseAmount);
                    break;
                default:
                    break;
            }
        }
    }
}
