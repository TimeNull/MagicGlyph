using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicGlyphs.UI
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

        public enum Canvas
        {
            WIN, LOSE, MENU, PAUSEICON
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

        public void ActiveCanvas(Canvas canvas)// active specific canvas
        {
            switch (canvas)
            {
                case Canvas.WIN:
                    win.SetActive(true);
                    pauseIcon.SetActive(false);
                    break;
                case Canvas.LOSE:
                    lose.SetActive(true);
                    pauseIcon.SetActive(false);
                    break;
                case Canvas.MENU:
                    menu.SetActive(true);
                    pauseIcon.SetActive(false);
                    break;
                case Canvas.PAUSEICON:
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

        public void Exit()
        {
            Application.Quit();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!joystick) joystick = GameObject.FindWithTag("Joystick");

            switch (scene.buildIndex)
            {
                case 0:
                    if(cameras.activeSelf) cameras.SetActive(false);
                    if (player.activeSelf) player.SetActive(false);
                    if (portal.activeSelf) portal.SetActive(false);
                    ActiveCanvas(Canvas.MENU);
                    if (joystick) joystick = null;
                    break;
                case 1:
                    player.SetActive(true);
                    cameras.SetActive(true);
                    portal.SetActive(true);
                    break;
                case 2:
                    ActiveCanvas(Canvas.PAUSEICON);
                    break;

            }
        }


    }

}
