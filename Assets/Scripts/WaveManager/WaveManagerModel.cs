using UnityEngine;
using UnityEngine.UI;

public class WaveManagerModel { 
    public Spawn[] _spawns;
    public static int _currentCountEnemies = 0;
    private float _timeForPauseBetweenWaves = 10f;
    public Text TextOfCountEnemies { get; set; }
    public int TotalLimitEnemies { get; set; }
    public int CountOfWaves { get; set; }
    public float CurrentTimeForPauseBetweenWaves { get; set; }
    public float TimeForPauseBetweenWave => _timeForPauseBetweenWaves;
    public static bool ChangePlatforms { get; set; }
    public bool IsPause { get; set; }

}
