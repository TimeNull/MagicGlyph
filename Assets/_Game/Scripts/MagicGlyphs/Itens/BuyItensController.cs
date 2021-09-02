using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace MagicGlyphs
{
    [Serializable]
    public class ItensArray
    {
        public GameObject Item;
        public Sprite ImageItem;
        public int PriceItem;
        public string descricao;
    }

    public class BuyItensController : MonoBehaviour
    {
        public static GMDelegate buyItemDelegate;
        [SerializeField] List<ItensArray> Itens;
        public static ItensArray currentItem;
        [SerializeField] Image itemImageCanvas;
        [SerializeField] Text coinsPriceCanvas, descricaoItem;

        private void Awake()
        {
            PassLevelManage.defeatLevel -= DrawItem;
            buyItemDelegate -= BuyItem;
            PassLevelManage.defeatLevel += DrawItem;
            buyItemDelegate += BuyItem;
            
        }

        void DrawItem() //sorteia um item que está dentro da array do objeto e já coloca suas informações no canvas para o player
        {
            currentItem = Itens[UnityEngine.Random.Range(0, Itens.Count)];
            itemImageCanvas.sprite = currentItem.ImageItem;
            coinsPriceCanvas.text = "" + currentItem.PriceItem;
            descricaoItem.text = "" + currentItem.descricao;
        }

        void BuyItem() //quando o item é comprado, aqui ele retira o item da Lista de itens possiveis para aparecer
        {
            for (int i = 0; i < Itens.Count; i++)
            {
                if (currentItem.Item.GetComponent<ItemProperties>().IdItem == Itens[i].Item.GetComponent<ItemProperties>().IdItem)
                {
                    Itens.Remove(Itens[i]);
                }
            }
        }


    }
}
