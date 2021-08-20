using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void LevelsManagerDelegate();

public class MainLevelsManager : MonoBehaviour
{
    [SerializeField] int fasesQuantityPerWorld;
    int fasesCounter;

    public static LevelsManagerDelegate NextWorldEvent;
    public static LevelsManagerDelegate NextLevelEvent;
    public static LevelsManagerDelegate ResetRunEvent;

    [SerializeField] UnityEngine.Object transitionScene; //cena de loading
    [SerializeField] FasesHandler handler; //SO com os leveis de cada mundo
    List<int> availableLevels = new List<int>(); //lista de niveis disponíveis, para não repetir level e ser aleatório
    int actualWorld; //int para registrar de que mundo os leveis devem ser pegos

    private void Awake()
    {
        fasesCounter = fasesQuantityPerWorld;

        NextWorldEvent = ChangeWorld;
        NextLevelEvent = NextLevel;
        ResetRunEvent = ResetRun;

      
        availableLevels = new List<int>();

        for (int i = 0; i < handler.Worlds[actualWorld].levels.Length; i++)
        {
            availableLevels.Add(i);
        }
    }

    public void ChangeWorld()
    {
        fasesCounter = fasesQuantityPerWorld;

        actualWorld++;
        availableLevels = new List<int>();

        for (int i = 0; i < handler.Worlds[actualWorld].levels.Length; i++)
        {
            availableLevels.Add(i);
        }
    }

    public void NextLevel()
    {
        fasesCounter--;

        if (fasesCounter >= 0)
        {
            int newIndex = Random.Range(0, availableLevels.Count);
            int newLevel = availableLevels[newIndex];
            availableLevels.RemoveAt(newIndex);

            SceneManager.LoadScene(transitionScene.name);
            SceneManager.LoadSceneAsync(handler.Worlds[actualWorld].levels[newLevel].name);
        }
        
        if (fasesCounter < 0)
        {
            SceneManager.LoadScene(transitionScene.name);
            SceneManager.LoadSceneAsync(handler.Worlds[actualWorld].Boss[actualWorld].name);
        }
    }

    public void ResetRun()
    {
        actualWorld = 0;
    }
}
