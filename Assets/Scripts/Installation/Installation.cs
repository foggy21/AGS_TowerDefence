using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installation : MonoBehaviour
{
    [SerializeField] private GlobalEventManager _globalEventManager;
    private void Awake()
    {
        Instantiate(_globalEventManager, Vector2.zero, Quaternion.identity);
    }
}
