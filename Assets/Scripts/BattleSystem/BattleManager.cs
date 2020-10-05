using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    
    [SerializeField] private Wave wave;
    private enum State {
        Idle,
        Active
    }
    private State state;
    private void startBattle() {
        state = State.Active;
        wave.SpawnEnemies();
    }

    private void Awake()
    {
        state = State.Idle;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        startBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // wave controller
    [System.Serializable]
    private class Wave{
        int count = 0;
        [SerializeField] private GameObject[] enemys;

        public void SpawnEnemies() {
            foreach (GameObject enemy in enemys) {
                enemy.GetComponent<EnemyManager>().Spawn();
            }
        }

    }
}


