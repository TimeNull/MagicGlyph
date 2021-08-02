using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MagicGlyphs
{
    public delegate void CoinsDLG(int number);
    public class CoinsManager : MonoBehaviour
    {
        public static int currentCoins;

        public static CoinsDLG addCoins, removeCoins;
        [SerializeField] Text coinsUI;

        private void OnEnable()
        {
            addCoins = AddCoins;
            removeCoins = RemoveCoins;
        }

        public void AddCoins(int qtde)
        {
            currentCoins += qtde;
            coinsUI.text = "" + currentCoins;
        }

        public void RemoveCoins (int qtde)
        {
            currentCoins -= qtde;
            coinsUI.text = "" + currentCoins;
        }
    }
}
