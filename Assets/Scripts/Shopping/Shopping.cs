using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{

    public GameObject shopPanel;

    // public void shopTower()
    // {

    //             shopPanel.SetActive(true);
    //             Debug.Log("Click shop");

    // }

    void OnMouseDown()
    {
        shopPanel.SetActive(true);
        Debug.Log("Click shop");
    }





}
