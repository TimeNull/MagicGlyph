using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MagicGlyphs
{
    public class LifeCanvas : MonoBehaviour
    {
        [SerializeField] Life life;

        TextMeshPro tmpro;

        private void Start()
        {
            tmpro.text = life.MaxLife.ToString();
        }

        public void ChangeLife()
        {
            tmpro.text = life.ActualLife.ToString();
        }
    }
}
