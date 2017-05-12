using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]//need to edit inside Unity
	public class Wave {
        public string name;
        public Transform enemyMid;
        public Transform enemyLeft;
        public Transform enemyRight;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public int NextWave {
        get { return nextWave + 1; }
    }

    public float timeBetweenWaves = 5.0f;
    private float waveCountDown;
    public float WaveCountdown {
        get { return waveCountDown; }
    }

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State {
        get { return state; }
    }

    void Start() {
        waveCountDown = timeBetweenWaves;
    }

    void changeState(SpawnState newState) {
        state = newState;
    }

    void Update() {

        if (state == SpawnState.WAITING) {
            if (!EnemyIsAlive()) {
                WaveCompleted();
            }
            else {
                return;
            }
        }

        if (waveCountDown <= 0) {
            if (state != SpawnState.SPAWNING) {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else {
            waveCountDown -= Time.deltaTime;
        }
    }


    void WaveCompleted() {
        Debug.Log("wave completed!");
        
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        if(nextWave + 1 > waves.Length - 1) {
            nextWave = 0;
            Debug.Log("All waves Complete! Now Looping...");
        }
        else {
            nextWave++;
        }
        
    }

    //Inefficient method during 60fps
    bool EnemyIsAlive() {
          searchCountdown -= Time.deltaTime;

          if (searchCountdown <= 0f) {
              searchCountdown = 1f;
              if (GameObject.FindGameObjectWithTag("enemy") == null) {
                  Debug.Log("No more enemies");
                  return false;

              }
          }

          return true;

         
        
    }

    IEnumerator SpawnWave(Wave wave) {
        Debug.Log("Spawning wave: " + wave.name);
        state = SpawnState.SPAWNING;
        
        //Spawn Stuff
        for(int i = 0; i < wave.count; i++) {
            SpawnEnemy(wave.enemyMid);
            SpawnEnemy(wave.enemyLeft);
            SpawnEnemy(wave.enemyRight);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy) {
        Debug.Log("Spawning Enemy:" + enemy.name);
        Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
        
    }

}
