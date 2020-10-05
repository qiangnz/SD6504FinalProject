using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerRanger : MonoBehaviour
{
    GameObject player; 
    public EnemyManager enemyManager; 
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player ) {
            enemyManager.SetTarget(player);
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject == player ) {
            enemyManager.SetTarget(null);
        }
    }
}
