using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DirectionManager : MonoBehaviour
{
    public event EventHandler<EventBusManager> OnReDirection;
    void OnTriggerEnter (Collider other)
    {
        // If enemy enter middle point notify to change endpoint
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy arrive middlePoint");
            if (OnReDirection != null)
                OnReDirection(this, new EventBusManager(other.gameObject));
        }
    }
}
