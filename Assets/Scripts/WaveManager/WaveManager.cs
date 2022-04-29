using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveManagerView _waveManagerView;
    private WaveManagerModel _waveManagerModel = new WaveManagerModel();

    private void Start()
    {
        Debug.Log("START");
        WaveManagerModel._currentCountEnemies = 0;
        _waveManagerModel.CurrentTimeForPauseBetweenWaves = _waveManagerModel.TimeForPauseBetweenWave;
        _waveManagerModel._spawns = _waveManagerView.GetAllSpawns();
        _waveManagerModel.CountOfWaves = 1;
        _waveManagerModel.IsPause = true;
        _waveManagerModel.TextOfCountEnemies = GameObject.FindGameObjectWithTag("TextOfCountEnemies").GetComponent<Text>();
    }

    private void Update()
    {
        if (_waveManagerModel.IsPause == false)
        {
            _waveManagerModel.TextOfCountEnemies.text = "Количество врагов: " + WaveManagerModel._currentCountEnemies.ToString();
        } 
        else
        {
            _waveManagerModel.CurrentTimeForPauseBetweenWaves -= Time.deltaTime;
        }
        if (WaveManagerModel._currentCountEnemies == 0 && _waveManagerModel.IsPause == false)
        {
            _waveManagerModel.IsPause = true;
            _waveManagerModel.CountOfWaves += 1;
            if (_waveManagerModel.CountOfWaves == 2)
            {
                WaveManagerModel.ChangePlatforms = true;
            }
            _waveManagerModel.CurrentTimeForPauseBetweenWaves = _waveManagerModel.TimeForPauseBetweenWave;
        } 
        else if (_waveManagerModel.CurrentTimeForPauseBetweenWaves <= 0 && _waveManagerModel.IsPause == true)
        {
            _waveManagerModel.IsPause = false;
            _waveManagerView.StartToSpawnEnemies(_waveManagerModel._spawns, _waveManagerModel.TotalLimitEnemies, ref WaveManagerModel._currentCountEnemies);
        }

    }

    public static void DecrementCountEnemies()
    {
        WaveManagerModel._currentCountEnemies--;
    }
}
