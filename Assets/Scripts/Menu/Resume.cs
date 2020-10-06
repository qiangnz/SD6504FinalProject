using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Resume : MonoBehaviour
{

        public GameObject pauseMenu;
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale=1f;
    }
}
