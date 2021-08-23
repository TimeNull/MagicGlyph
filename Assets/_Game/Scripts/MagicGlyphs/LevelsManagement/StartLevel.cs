using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class StartLevel : MonoBehaviour
    {
        private MainLevelsManager mainLevels;
        private void Awake()
        {
           GameObject.FindWithTag("GameManager").TryGetComponent(out mainLevels);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                mainLevels.NextLevel();
            }
        }

    }
}


