using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // caculate enemy
        if (other.gameObject.tag.Equals("Enemy")) {
            Debug.Log("Enemy arrive endPoint");
            Destroy(other.gameObject);
            // todo

        }

    }
}
