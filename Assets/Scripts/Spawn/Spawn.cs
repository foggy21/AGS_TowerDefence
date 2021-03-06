using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{ 
    [SerializeField] private string _enemyName;
    private int _enemiesOnScreen = 0;
    public int LimitEnemies { get; set;}
    private GameObject Enemy { get; set; }

    private void Awake()
    {
        Enemy = Resources.Load<GameObject>(_enemyName);
    }

    public IEnumerator SpawnEnemy()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
        _enemiesOnScreen++;
        yield return new WaitForSeconds(Random.Range(0.75f, 2f));
        if (_enemiesOnScreen < LimitEnemies)
        {
            StartCoroutine(SpawnEnemy());
        }
        else
        {
            _enemiesOnScreen = 0;
        }
    }
}
