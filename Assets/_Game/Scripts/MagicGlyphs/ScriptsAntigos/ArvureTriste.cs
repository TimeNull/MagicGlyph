using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicGlyphs
{
    public class ArvureTriste : MonoBehaviour
    {
        
        bool onCoolDown;
        [SerializeField] GameObject granade;
        [SerializeField] float coolDownTime;
        ScriptBostaMobsPegaPlayer getPlayer;

        // Start is called before the first frame update
        void Start()
        {
            getPlayer = GetComponent<ScriptBostaMobsPegaPlayer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!onCoolDown && getPlayer.jogador)
            {
                StartCoroutine(ThrowGranade());
                GameObject a = Instantiate(granade, transform.position, Quaternion.identity);
                a.GetComponent<ArvureTristeBolota>().player = getPlayer.jogador.transform;
            }
        }


        IEnumerator ThrowGranade()
        {
            onCoolDown = true;
            yield return new WaitForSeconds(coolDownTime);
            onCoolDown = false;
        }
    }
}
