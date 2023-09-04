using System;
using Internal.Scripts.WaveSpawner;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelManager : MonoBehaviour
{
    private int sceneIndex;

    public event Action OnNextLevel;

    private string levelMap = "LevelMap";

    private NewWaveSpawner newWaveSpawner;

    [Inject]
    private void Construct(NewWaveSpawner newWaveSpawner)
    {
        this.newWaveSpawner = newWaveSpawner;
    }

    private void OnEnable()
    {
        newWaveSpawner.OnNextLevel += ReloadScene;
    }

    private void OnDisable()
    {
        newWaveSpawner.OnNextLevel -= ReloadScene;
    }

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void NextLevel()
    {
        OnNextLevel?.Invoke();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void LoadLevelMap()
    {
        SceneManager.LoadScene(levelMap);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public int ReturnSceneIndex()
    { 
        return sceneIndex;
    }

}
