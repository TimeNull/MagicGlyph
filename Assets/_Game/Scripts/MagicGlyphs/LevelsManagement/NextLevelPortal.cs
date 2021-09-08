using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{

    public class NextLevelPortal : MonoBehaviour
    {
        [SerializeField] GameObject particlesPortal;
        [SerializeField] GameObject chest;
        [SerializeField] GameObject PortalObj;

        new private static Collider collider;

        private void Awake()
        {
            collider = GetComponent<Collider>();
            
            collider.enabled = false; 


        }

        public void ActivatePortal()
        {
            Debug.Log("chamou sdiqsoisqoi");
            PortalObj.SetActive(true);
            collider.enabled = true;
            particlesPortal.SetActive(true);
            chest.SetActive(true);
        }

        public void DeactivatePortal()
        {
            Debug.Log("deschamou sdiqsoisqoi");
            PortalObj.SetActive(false);
            collider.enabled = false;
            particlesPortal.SetActive(false);
            chest.SetActive(false);
        }

        private void OnEnable()
        {
            GameObject ok = GameObject.FindGameObjectWithTag("PortalPoint");
            if (ok)
                PortalObj.transform.position = ok.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                MainLevelsManager.NextLevelEvent();
                DeactivatePortal();
            }
        }


    }
}
