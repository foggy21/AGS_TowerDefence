using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManagerView : MonoBehaviour
{
    public Spawn[] GetAllSpawns()
    {
        return FindObjectsOfType<Spawn>();
    }

    public void StartToSpawnEnemies(Spawn[] spawns, int limitEnemies, ref int currentCountEnemies)
    {
        for (int i = 0; i < spawns.Length; ++i)
        {
            limitEnemies = Random.Range(3, 5);
            currentCountEnemies += limitEnemies;
            SetLimitForEnemies(spawns[i], limitEnemies);
            StartCoroutine(spawns[i].SpawnEnemy());
        }
    }

    private void SetLimitForEnemies(Spawn spawn, int limitValue)
    {
        spawn.LimitEnemies = limitValue;
    }

}
