using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    
    [SerializeField] private Wave[] waves;
    private enum State {
        Idle,
        Active
    }
    private State state;
    private void startBattle() {
        state = State.Active;
        // wave.SpawnEnemies();
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
    private int index = 0;
    private Wave preWave = null;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("current wave:" + index);
        if (index < 6) {
            switch (state) {
                case State.Active:
                    Wave wave = waves[index];
                    if (!wave.IsInit) {
                        wave.SpawnEnemies();
                        preWave = wave;
                        Debug.Log("pre wave:" + index + preWave.isWaveOver() );
                    } else if (preWave != null && preWave.isWaveOver()) {
                        index++;
                    }
                    break;
            }
        }
    }


    // wave controller
    [System.Serializable]
    private class Wave{
        bool isInit = false;
        [SerializeField] private GameObject[] enemys;

        public void SpawnEnemies() {
            isInit = true;
            foreach (GameObject enemy in enemys) {
                enemy.GetComponent<EnemyManager>().Spawn();
            }
        }

        public bool IsInit{ get { return isInit; } }

        public bool isWaveOver() {
            if (isInit) {
                foreach (GameObject enemy in enemys) {
                    Debug.Log("enemy" + enemy.name + "isAlive:" + enemy.GetComponent<EnemyManager>().isAlive());
                    if (enemy != null && enemy.GetComponent<EnemyManager>().isAlive()) {
                        return false;
                    }
                }
                return true;
            } else {
                return false;
            }
        }

    }
}


