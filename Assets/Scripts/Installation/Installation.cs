using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installation : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;

    private void Awake()
    {
        Instantiate(_waveManager, Vector2.zero, Quaternion.identity);
    }
}
