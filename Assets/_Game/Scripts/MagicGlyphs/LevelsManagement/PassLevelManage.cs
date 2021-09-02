using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GMDelegate();
namespace MagicGlyphs
{
    public class PassLevelManage : MonoBehaviour
    {
        public static int enemiesQtde;

        public static event GMDelegate defeatLevel;

        private static bool bossLevel;

        private static bool defeated;
        
        [SerializeField] private NextLevelPortal _portal;

        private static NextLevelPortal portal;

        private void Awake()
        {
            portal = _portal;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0 || scene.buildIndex == 1)
                return;

            if (scene.buildIndex == 11)
            {
                bossLevel = true;
            }

            if (portal != null)
                portal.DeactivatePortal();

            enemiesQtde = 0;

            if (!bossLevel)
            {   
                enemiesQtde = GameObject.FindGameObjectsWithTag("Enemy").Length;
            }
            else
            {
                enemiesQtde = 1;
            }
        }

        public static void CheckEnemies() //called by Died() method on enemy controller
        {
            Debug.Log(enemiesQtde);
            enemiesQtde--;
            Debug.Log(enemiesQtde);

            if (enemiesQtde <= 0)
            {
                if (!bossLevel)
                {
                    portal.ActivatePortal();
                    defeated = true;
                }
                else
                {
                    GameManager.gameManager.DisableObjects();
                    GameManager.gameManager.ActiveCanvas((int)GameManager.CanvasName.WIN);
                }
            }
        }

        private void Update()
        {
            if (defeated)
            {
                defeated = false;
                Invoke("DefeatLevel", 0.5f);
            }
        }

        private void DefeatLevel()
        {
            defeatLevel?.Invoke();
        }

    }
}
