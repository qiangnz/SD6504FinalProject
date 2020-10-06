using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
    public void Helpgame()
    {
       SceneManager.LoadScene("HelpScene");
    }
}
