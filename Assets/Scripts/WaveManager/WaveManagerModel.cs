using UnityEngine;
using UnityEngine.UI;

public class WaveManagerModel { 
    public static int _currentCountEnemies = 0;
    private float _timeForPauseBetweenWaves = 20f;
    public Text TextOfCountEnemies { get; set; }
    public Text TextOfWaves { get; set; }
    public int TotalLimitEnemies { get; set; }
    public static int CountOfWaves { get; set; }
    public float CurrentTimeForPauseBetweenWaves { get; set; }
    public float TimeForPauseBetweenWave => _timeForPauseBetweenWaves;
    public static bool ChangePlatforms { get; set; }
    public bool IsPause { get; set; }

}
