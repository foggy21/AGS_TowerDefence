using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private Platform[] _platformMarkers;

    private void Awake()
    {
        _platformMarkers = FindObjectsOfType<Platform>();
    }

    private void Update()
    {
        if (WaveManagerModel.ChangePlatforms)
        {
            ChangePlatforms();
            WaveManagerModel.ChangePlatforms = false;
        }
    }

    private void ChangePlatforms()
    {
        Debug.Log(_platformMarkers.Length);
        for (int i = 0; i < _platformMarkers.Length; ++i)
        {
            StartCoroutine(_platformMarkers[i].ChangePlatform());
        }
    }
}
