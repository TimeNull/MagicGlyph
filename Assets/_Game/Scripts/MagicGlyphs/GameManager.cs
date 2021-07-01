using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace MagicGlyphs
{
    public class GameManager : MonoBehaviour
    {
        //static variable is only initialized once;
        private static GameManager gameManager;
        //Fitas de Loading, de Scenes, de UI tratar tudo o que for possível aqui
        [SerializeField] private GameObject player;
        private CinemachineVirtualCamera vcam;

        void Awake()
        {
            //only one instance please
            if (gameManager == null)
            {
                gameManager = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (gameManager != null)
            {
                Destroy(gameObject);
                return;
            }

        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {

        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex.Equals(0))
            {
                if (!GameObject.FindWithTag("Player"))
                    player = Instantiate(player);

                vcam = GameObject.Find("vcam1").GetComponent<CinemachineVirtualCamera>();
                vcam.Follow = player.transform;
            }

        }


        void Update()
        {

        }


    }
}




