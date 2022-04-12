using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPresenter : Tower
{
    void Start()
    {
        ListOfEnemies = new List<GameObject>();
        OffsetAttackX = 1f;
        OffsetAttackY = 2f;
        DistanceAttack = 10f;
        DelayAttack = 1f;
        CurrentDelayAttack = 0;
    }

    void FixedUpdate()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.left * DistanceAttack);
        Enemy = FindEnemy();
        if (Enemy != null)
        {
            if (!ListOfEnemies.Contains(Enemy))
            {
                ListOfEnemies = RegisterNewEnemy(ListOfEnemies, Enemy);
            }
            
        }

        if (TargetEnemy == null && ListOfEnemies.Count > 0)
        {
            TargetEnemy = ChooseEnemy(ListOfEnemies);
        }

        if (TargetEnemy != null)
        {
            if (CurrentDelayAttack <= 0)
            {
                Attack(TargetEnemy, "Fireball");
                CurrentDelayAttack = DelayAttack;
            }
            else
            {
                CurrentDelayAttack -= Time.deltaTime;
            }
            
        }

        if(TargetEnemy != null && Vector2.Distance(transform.position, TargetEnemy.transform.position) >= DistanceAttack*1.1f)
        {
            ListOfEnemies = UnregisterEnemy(ListOfEnemies, TargetEnemy);
            TargetEnemy = null;
            CurrentDelayAttack = 0;
        }
    }

    public override void Attack(GameObject enemy, string projectileName)
    {
        if (enemy != null)
        {
            Fireball projectile = Resources.Load<Fireball>(projectileName); 
            projectile.GetEnemy(enemy);

            if (enemy.transform.position.x > transform.position.x)
            {
                Instantiate(projectile, new Vector2(transform.position.x + OffsetAttackX, transform.position.y + OffsetAttackY), Quaternion.identity);
            }
            else
            {
                Instantiate(projectile, new Vector2(transform.position.x - OffsetAttackX, transform.position.y + OffsetAttackY), Quaternion.identity);
            }
            
        }
    }
}
