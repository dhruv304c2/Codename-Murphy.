using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public int Enemies;
    public int maxEnemies = 4;
    public float MaxIncreasePeriod = 1;
    public GameScore LevelManager;

    private void Start()
    {
        StartCoroutine(IncreaseLimit());       
    }

    EnemyCounter()
    {
        Enemies = 0;
    }

    public void CountUp()
    {
        Enemies++;
    }

    public void CountDown()
    {
        Enemies--;
        LevelManager.CountUp();
    }

    public int GetCount()
    {
        return Enemies;
    }

    IEnumerator IncreaseLimit()
    {
        maxEnemies++;
        yield return new WaitForSeconds(MaxIncreasePeriod);
        StartCoroutine(IncreaseLimit());
    }
}
