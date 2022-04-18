using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public GameObject Enemy { get; set; }
    public GameObject CurrentEnemy { get; set; }
    protected float Damage { get; set; }

    public void SetEnemy(GameObject enemy)
    {
        Enemy = enemy;
    }
}
