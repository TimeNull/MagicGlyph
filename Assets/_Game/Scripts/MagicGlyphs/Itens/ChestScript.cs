using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class ChestScript : MonoBehaviour
    {
        [SerializeField] GameObject itemCanvas;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && BuyItensController.currentItem != null)
            {
                itemCanvas.SetActive(true);
            }
        }
    }
}
