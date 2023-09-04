using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int sceneIndex;

    public event Action OnNextLevel;

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

    public void Quit()
    {
        Application.Quit();
    }

    public int ReturnSceneIndex()
    { 
        return sceneIndex;
    }

}
