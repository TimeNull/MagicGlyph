using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyph
{

    public class NextLevelPortal : MonoBehaviour
    {
        [SerializeField] GameObject particlesPortal;
        [SerializeField] GameObject chest;
        [SerializeField] GameObject PortalObj;

        private void Start()
        {
            DontDestroyOnLoad(transform.parent.gameObject);
            PortalObj.transform.position = GameObject.FindGameObjectWithTag("PortalPoint").transform.position;

            this.GetComponent<Collider>().enabled = false;

            MagicGlyphs.PassLevelManage.defeatLevel -= ActivePortal;
            MagicGlyphs.PassLevelManage.defeatLevel += ActivePortal;
        }

        void ActivePortal()
        {
            this.GetComponent<Collider>().enabled = true;
            particlesPortal.SetActive(true);
            chest.SetActive(true);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                MainLevelsManager.NextLevelEvent();
            }
        }
    }
}
