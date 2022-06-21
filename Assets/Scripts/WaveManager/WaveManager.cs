using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveManagerView _waveManagerView;
    private WaveManagerModel _waveManagerModel = new WaveManagerModel();
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private Spawn[] _spawns;

    private void Start()
    {
        Debug.Log("START");
        WaveManagerModel._currentCountEnemies = 0;
        _waveManagerModel.TextOfWaves = GameObject.FindGameObjectWithTag("TextOfWaves").GetComponent<Text>();
        _waveManagerModel.CurrentTimeForPauseBetweenWaves = _waveManagerModel.TimeForPauseBetweenWave;
        _spawns[4].gameObject.SetActive(false);
        _spawns[5].gameObject.SetActive(false);
        WaveManagerModel.CountOfWaves = 1;
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
            WaveManagerModel.CountOfWaves += 1;
            if (WaveManagerModel.CountOfWaves == 3 || WaveManagerModel.CountOfWaves == 5 || WaveManagerModel.CountOfWaves == 6)
            {
                _spawns[4].gameObject.SetActive(true);
                _spawns[5].gameObject.SetActive(true);
            }
            else
            {
                _spawns[4].gameObject.SetActive(false);
                _spawns[5].gameObject.SetActive(false);
            }
            if (WaveManagerModel.CountOfWaves == 3)
            {
                WaveManagerModel.ChangePlatforms = true;
            }
            if (WaveManagerModel.CountOfWaves == 7)
            {
                Time.timeScale = 0;
                _winMenu.SetActive(true);
            }
            else
            {
                _waveManagerModel.TextOfWaves.text = "Волны: " + WaveManagerModel.CountOfWaves.ToString() + "/6";
            }
            _waveManagerModel.CurrentTimeForPauseBetweenWaves = _waveManagerModel.TimeForPauseBetweenWave;
        } 
        else if (_waveManagerModel.CurrentTimeForPauseBetweenWaves <= 0 && _waveManagerModel.IsPause == true)
        {
            _waveManagerModel.IsPause = false;
            _waveManagerView.StartToSpawnEnemies(_spawns, _waveManagerModel.TotalLimitEnemies, ref WaveManagerModel._currentCountEnemies);
        }

    }

    public static void DecrementCountEnemies()
    {
        WaveManagerModel._currentCountEnemies--;
    }
}
