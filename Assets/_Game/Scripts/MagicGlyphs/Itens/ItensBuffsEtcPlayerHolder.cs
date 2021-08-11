using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class ItensBuffsEtcPlayerHolder : MonoBehaviour
    {
        public static int[] ActiveItensIds;
        [SerializeField]List<GameObject> ItensPlayerArray;
        private void Start()
        {
            MagicGlyphs.BuyItensController.buyItemDelegate -= BuyItem;
            MagicGlyphs.BuyItensController.buyItemDelegate += BuyItem;

            ItensPlayerArray = new List<GameObject>();

            int childs = transform.childCount;

            for (int i = 0; i < childs; i++)
            {
                ItensPlayerArray.Add(transform.GetChild(i).gameObject);
            }

           
        }

        void BuyItem() //pega o item que foi sorteado e procura por um com o mesmo ID na lista de itens do player para ativar
        {
            foreach (GameObject item in ItensPlayerArray)
            {
                if (BuyItensController.currentItem.Item.GetComponent<ItemProperties>().IdItem == item.GetComponent<ItemProperties>().IdItem)
                {
                    item.SetActive(true);
                }
            }
        }
    }
}
