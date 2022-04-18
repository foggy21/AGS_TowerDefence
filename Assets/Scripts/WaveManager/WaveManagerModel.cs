using UnityEngine;
using UnityEngine.UI;

public class WaveManagerModel { 
    public Spawn[] _spawns;
    public int _currentCountEnemies = 0;
    private float _timeForPauseBetweenWaves = 10f;
    public Text _textOfCountEnemies { get; set; }
    public int TotalLimitEnemies { get; set; }
    public float CurrentTimeForPauseBetweenWaves { get; set; }
    public float TimeForPauseBetweenWave => _timeForPauseBetweenWaves;

    public bool IsPause { get; set; }

}
