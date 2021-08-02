using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class ScriptBostaMobsPegaPlayer : MonoBehaviour
    {
        [HideInInspector]
        public Transform jogador;

        [Header("acha o player")]
        private Collider[] OnRange;
        [SerializeField] private LayerMask layersAfected;
        private float minDist;
        private int getNearestObject = 0;
        [SerializeField] private float range;


        // Update is called once per frame
        void LateUpdate()
        {
            OnRangeEnter();
        }



        //check if enemies entered the attack range
        void OnRangeEnter()
        {
            OnRange = Physics.OverlapSphere(transform.position, range, layersAfected);

            if (jogador)
            {
                if (Vector3.Distance(jogador.position, transform.position) > range || !jogador.gameObject.activeSelf)
                {
                    jogador = null;
                }
            }
            
            if (!jogador)
            {
                GetNewGameObjectOnRange();
            }

        }

        void GetNewGameObjectOnRange()
        {
            if (OnRange.Length > 0)
            {

                for (int i = 0; i < OnRange.Length; i++)
                {
                    if (i == 0)
                    {
                        minDist = Vector3.Distance(transform.position, OnRange[i].transform.position);
                        getNearestObject = i;
                    }
                    else if (Vector3.Distance(transform.position, OnRange[i].transform.position) < minDist)
                    {
                        minDist = Vector3.Distance(transform.position, OnRange[i].transform.position);
                        getNearestObject = i;
                    }
                }

                jogador = OnRange[getNearestObject].gameObject.transform;

            }
            else
            {
                jogador = null;
            }
        }
    }
}
