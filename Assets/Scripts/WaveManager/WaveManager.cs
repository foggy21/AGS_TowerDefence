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
        _waveManagerModel.CurrentTimeForPauseBetweenWaves = _waveManagerModel.TimeForPauseBetweenWave;
        _waveManagerModel._spawns = _waveManagerView.GetAllSpawns();
        _waveManagerModel.IsPause = true;
        _waveManagerModel._textOfCountEnemies = GameObject.FindGameObjectWithTag("TextOfCountEnemies").GetComponent<Text>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_waveManagerModel.IsPause == false)
        {
            _waveManagerModel._textOfCountEnemies.text = _waveManagerModel._currentCountEnemies.ToString();
        } else
        {
            _waveManagerModel.CurrentTimeForPauseBetweenWaves -= Time.deltaTime;
        }
        if (_waveManagerModel._currentCountEnemies == 0 && _waveManagerModel.IsPause == false)
        {
            _waveManagerModel.IsPause = true;
            _waveManagerModel.CurrentTimeForPauseBetweenWaves = _waveManagerModel.TimeForPauseBetweenWave;
        } 
        else if (_waveManagerModel.CurrentTimeForPauseBetweenWaves <= 0 && _waveManagerModel.IsPause == true)
        {
            _waveManagerModel.IsPause = false;
            _waveManagerView.StartToSpawnEnemies(_waveManagerModel._spawns, _waveManagerModel.TotalLimitEnemies, ref _waveManagerModel._currentCountEnemies);
        }
    }
}
