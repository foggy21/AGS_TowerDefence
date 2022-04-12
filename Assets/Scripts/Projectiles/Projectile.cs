using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public static GameObject Enemy { get; set; }
    protected float Damage { get; set; }


    public void GetEnemy(GameObject enemy)
    {
        Enemy = enemy;
    }


}
