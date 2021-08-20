using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public sealed class DontDestroyOnLoadObjects : MonoBehaviour
    {
        private static DontDestroyOnLoadObjects objects { get; set; } //almost a singleton, to prevent clones of this object

        private void Awake()
        {
            if (objects != null && objects != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                objects = this;
            }
            DontDestroyOnLoad(gameObject);
        }

    }
}

