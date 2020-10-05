using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpearKillEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag.Equals("Enemy")) {
            Debug.Log("Enemy:" + other.gameObject.name + "be hurted");
            EnemyManager enemy =  other.gameObject.GetComponent<EnemyManager>();
            enemy.TakeDamage(10);
        }
    }
}
