using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
public void StartGame()
    {
       SceneManager.LoadScene("Level1");
       Time.timeScale=1;
    }
}
