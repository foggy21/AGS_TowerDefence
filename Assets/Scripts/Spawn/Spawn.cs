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
        Debug.Log(LimitEnemies);
        Instantiate(Enemy, transform.position, Quaternion.identity);
        _enemiesOnScreen++;
        yield return new WaitForSeconds(Random.Range(1f, 5f));
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
