using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void StartGame()
    {
       SceneManager.LoadScene("Level 1");
       Time.timeScale=1;
    }

    public void Helpgame()
    {
        SceneManager.LoadScene("Help");
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale=0f;
        Debug.Log("Clicked Pause");

    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale=1f;
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
