using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagicGlyphs
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] GameObject canvasBau;

        public void Buy()
        {
            if (CoinsManager.currentCoins >= BuyItensController.currentItem.PriceItem)
            {
                BuyItensController.buyItemDelegate();
                CoinsManager.removeCoins(BuyItensController.currentItem.PriceItem);
                canvasBau.SetActive(false);
                BuyItensController.currentItem = null;
            }
        }

        public void HaskCoins()
        {
            CoinsManager.addCoins(100);
        }

        public void Back()
        {
            canvasBau.SetActive(false);
        }
    }
}
