using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagicGlyphs
{
    public class LifeBar : MonoBehaviour
    {
        Slider uiLife;

        private void Awake()
        {
            uiLife = GetComponentInChildren<Slider>();
        }

        public void SetMaxLifeUI(Life value)
        {

            if (!ReferenceEquals(uiLife, null))
                uiLife.maxValue = value.MaxLife;
        }

        public void SetActualLifeUI(Life value)
        {
            
            if (!ReferenceEquals(uiLife, null))
                uiLife.value = value.ActualLife;
        }
    }
}
