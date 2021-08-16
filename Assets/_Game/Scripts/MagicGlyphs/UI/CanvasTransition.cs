using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasTransition : MonoBehaviour
{
    private Image image;
    private Color color;

    public static event GMDelegate transitionEnded;

    private bool fadeIn = true, fadeOut = false;

    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        color = image.color;
    }

    private void Update()
    {
        if (fadeIn)
            FadeIn();
        if (fadeOut)
            FadeOut();
       
    }

    private void FadeIn()
    {
        if (image.color.a < 1)
        {
            //Debug.Log("chamou");
            color.a *= 1.035f;
            image.color = color;
        }
        else
        {
            fadeIn = false;
            transitionEnded?.Invoke();
        }
    }

    private void FadeOut()
    {
        if (image.color.a > 0.01f)
        {
            color.a /= 1.01f;
            image.color = color;
        }
        else
        {
            fadeOut = false;
            gameObject.SetActive(false);
        }
    }

    private void Switch(Scene scene, LoadSceneMode mode)
    {
        fadeOut = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Switch;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Switch;
    }

}
