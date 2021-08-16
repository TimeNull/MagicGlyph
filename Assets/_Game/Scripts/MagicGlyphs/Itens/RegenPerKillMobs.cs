using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class RegenPerKillMobs : MonoBehaviour
    {
        public static bool canRegen;

        private void OnEnable()
        {
            canRegen = true;
        }
    }
}
