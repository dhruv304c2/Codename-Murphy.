using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minTimeBetweenSpawn = 5f, maxSpawnBetweenSpawn = 15f;
    EnemyCounter enemyCounter;
    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = FindObjectOfType<EnemyCounter>();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    { 
        if(enemyCounter.GetCount() < enemyCounter.maxEnemies)
        {
            var e = Instantiate(enemyPrefab, transform);
            e.transform.position = transform.position;
            enemyCounter.CountUp();
        }
        var time = Random.Range(minTimeBetweenSpawn, maxSpawnBetweenSpawn);
        yield return new WaitForSeconds(time);
        StartCoroutine(Spawn());
    }
}
