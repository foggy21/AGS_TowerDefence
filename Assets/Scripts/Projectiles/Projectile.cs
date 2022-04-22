using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public MoveableEnemy Enemy { get; set; }
    public MoveableEnemy CurrentEnemy { get; set; }
    protected int Damage { get; set; }

    public void SetEnemy(MoveableEnemy enemy)
    {
        Enemy = enemy;
    }
}
