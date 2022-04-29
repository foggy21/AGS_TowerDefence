using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installation : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    [SerializeField] private GlobalEventManager _globalEventManager;
    private void Awake()
    {

        Instantiate(_waveManager, Vector2.zero, Quaternion.identity);
        Instantiate(_globalEventManager, Vector2.zero, Quaternion.identity);
    }
}
