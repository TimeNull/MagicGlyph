using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GMDelegate();
namespace MagicGlyphs
{
    public class PassLevelManage : MonoBehaviour
    {
        private static int enemiesQtde;

        public static event GMDelegate defeatLevel;

        private static bool alreadyChecked;

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

            enemiesQtde = 0;
            enemiesQtde = GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        public static void CheckEnemies() //called by Died() method on enemy controller
        {
            enemiesQtde--;
            if (enemiesQtde <= 0)
            {
                if (!alreadyChecked)
                    defeatLevel?.Invoke();

                alreadyChecked = true;
            }
            else
            {
                alreadyChecked = false;
            }
        }
    }
}
