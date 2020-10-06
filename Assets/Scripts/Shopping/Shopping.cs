using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    public GameObject shopPanel;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shopPanel.SetActive(true);
        }
    }

}
