using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManagerView : MonoBehaviour
{
    public void StartToSpawnEnemies(Spawn[] spawns, int limitEnemies, ref int currentCountEnemies)
    {
        for (int i = 0; i < spawns.Length; ++i)
        {
            if (spawns[i].isActiveAndEnabled)
            {
                if (i <= 1 && WaveManagerModel.CountOfWaves == 1)
                {
                    limitEnemies = 3;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 1 && i <= 3) && WaveManagerModel.CountOfWaves == 1)
                {
                    limitEnemies = 7;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if (i <= 1 && WaveManagerModel.CountOfWaves == 2)
                {
                    limitEnemies = 4;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 1 && i <= 3) && WaveManagerModel.CountOfWaves == 2)
                {
                    limitEnemies = 9;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if (i <= 1 && WaveManagerModel.CountOfWaves == 3)
                {
                    limitEnemies = 5;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 1 && i <= 3) && WaveManagerModel.CountOfWaves == 3)
                {
                    limitEnemies = 10;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 3 && i <= 5) && WaveManagerModel.CountOfWaves == 3)
                {
                    limitEnemies = 10;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }
                
                if (i <= 1 && WaveManagerModel.CountOfWaves == 4)
                {
                    limitEnemies = 6;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 1 && i <= 3) && WaveManagerModel.CountOfWaves == 4)
                {
                    limitEnemies = 14;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if (i <= 1 && WaveManagerModel.CountOfWaves == 5)
                {
                    limitEnemies = 6;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 1 && i <= 3) && WaveManagerModel.CountOfWaves >=5)
                {
                    limitEnemies = 15;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 3 && i <= 5) && WaveManagerModel.CountOfWaves == 5)
                {
                    limitEnemies = 12;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if (i <= 1 && WaveManagerModel.CountOfWaves == 6)
                {
                    limitEnemies = 8;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }

                if ((i > 3 && i <= 5) && WaveManagerModel.CountOfWaves == 6)
                {
                    limitEnemies = 15;
                    SetWaveForSpawn(spawns[i], limitEnemies, ref currentCountEnemies);
                }


            }
        }
    }

    private void SetWaveForSpawn(Spawn spawn, int limitValue, ref int currentCountEnemies)
    {
        currentCountEnemies += limitValue;
        SetLimitForEnemies(spawn, limitValue);
        StartCoroutine(spawn.SpawnEnemy());
    }

    private void SetLimitForEnemies(Spawn spawn, int limitValue)
    {
        spawn.LimitEnemies = limitValue;
    }

    

}
