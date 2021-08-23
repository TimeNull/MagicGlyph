using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicGlyphs
{
    public sealed class GameManager : MonoBehaviour 
    {
        public static GameManager gameManager { get; private set; } // singleton (just for experimentation)

        [Space(10f)]
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject cameras;
        [SerializeField] private GameObject portal;
        [Space(20f)]
        [SerializeField] private GameObject transition;
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject pause;
        [SerializeField] private GameObject win;
        [SerializeField] private GameObject lose;
        [SerializeField] private GameObject pauseIcon;

        private GameObject joystick;

        private int targetIndex;

        private void Awake()
        {
            if (gameManager != null && gameManager != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                gameManager = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public enum CanvasName
        {
            WIN, LOSE, MENU, PAUSEICON
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GoToScene(11);
            }
        }

        private void OnEnable()
        {
            CanvasTransition.transitionEnded += TransitionEnded;
            SceneManager.sceneLoaded += OnSceneLoaded;        
        }

        private void OnDisable()
        {
            CanvasTransition.transitionEnded -= TransitionEnded;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void GoToSceneWithTransition(int index)
        {
            Debug.Log("called");
            targetIndex = index;
            transition.SetActive(true);
        }

        private void TransitionEnded()
        {
           
            Check();
            GoToScene(targetIndex);
        }

        public void GoToScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        private void Check() // reset active canvas
        {
            if (menu.activeSelf)
                menu.SetActive(false);
            if (pause.activeSelf)
                pause.SetActive(false);
            if (win.activeSelf)
                win.SetActive(false);
            if (lose.activeSelf)
                lose.SetActive(false);
        }

        public void ActiveCanvas(int canvas)// active specific canvas
        {
            switch ((CanvasName)canvas)
            {
                case CanvasName.WIN:
                    win.SetActive(true);
                    pauseIcon.SetActive(false);
                    break;
                case CanvasName.LOSE:
                    lose.SetActive(true);
                    pauseIcon.SetActive(false);
                    break;
                case CanvasName.MENU:
                    menu.SetActive(true);
                    pauseIcon.SetActive(false);
                    break;
                case CanvasName.PAUSEICON:
                    pauseIcon.SetActive(true);
                    break;

                default:
                    break;
            }
        }

        public void Pause(bool pause)
        {
            if (pause && Time.timeScale != 0)
            {
                if(joystick) joystick.SetActive(false);

                Time.timeScale = 0;
                this.pause.SetActive(true);
            }
            else if (Time.timeScale != 1)
            {
                if (joystick) joystick.SetActive(true);

                Time.timeScale = 1;
                this.pause.SetActive(false);
            }
               
        }

        public void DisableObjects()
        {
            player.SetActive(false);
            portal.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("chamou onscene loaded");
            if (!joystick) joystick = GameObject.FindWithTag("Joystick");

            switch (scene.buildIndex)
            {
                case 0:
                    portal.SetActive(true);
                    if(cameras.activeSelf) cameras.SetActive(false);
                    if (player.activeSelf) player.SetActive(false);
                    if (portal.activeSelf) portal.SetActive(false);
                    ActiveCanvas((int)CanvasName.MENU);
                    if (joystick) joystick = null;
                    break;
                case 1:
                    player.SetActive(true);
                    cameras.SetActive(true);
                    break;

            }

            if(scene.buildIndex != 0 && scene.buildIndex != 1 && scene.buildIndex != 12)
            {
                ActiveCanvas((int)CanvasName.PAUSEICON);
                player.transform.position = GameObject.FindWithTag("PlayerPoint").transform.position;
            }
               
        }


    }

}
