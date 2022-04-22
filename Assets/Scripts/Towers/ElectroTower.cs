using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroTower : Tower
{
    // Start is called before the first frame update
    void Start()
    {
        Enemy = null;
        OffsetAttackX = 1f;
        OffsetAttackY = 1.2f;
        DistanceAttack = 8f;
        BuildingProhibitionDistance = 5f;
        DelayAttack = 2f;
    }

    void FixedUpdate()
    {
        //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.right * DistanceAttack);
        DisablePlayerBuildSkill();
        if (Enemy == null)
        {
            Enemy = FindEnemy();
            CurrentDelayAttack = 0;
        }

        if (Enemy != null && Vector2.Distance(transform.position, Enemy.transform.position) < DistanceAttack)
        {
            if (CurrentDelayAttack <= 0)
            {
                Attack(Enemy);
                CurrentDelayAttack = DelayAttack;
            } 
            else
            {
                CurrentDelayAttack -= Time.deltaTime;
            }
        } 
        else if (Enemy != null && Vector2.Distance(transform.position, Enemy.transform.position) >= DistanceAttack)
        {
            Enemy = null;
        }
    }

    public override void Attack(MoveableEnemy enemy)
    {
        if (enemy != null)
        {
            Zap projectile = Resources.Load<Zap>("Zap");
            Instantiate(projectile, enemy.gameObject.transform);
            GlobalEventManager.SetNewStats(1, 3, DistanceAttack);
        }
    }

    public override MoveableEnemy FindEnemy()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.left, DistanceAttack, EnemyMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.right, DistanceAttack, EnemyMask);

        if (raycastLeft.collider != null && raycastRight.collider != null)
        {
            if (Vector2.Distance(transform.position, raycastRight.transform.position) > Vector2.Distance(transform.position, raycastLeft.transform.position))
            {
                return raycastLeft.collider.gameObject.GetComponent<MoveableEnemy>();

            }
            else
            {
                return raycastRight.collider.gameObject.GetComponent<MoveableEnemy>();
            }
        }
        else if (raycastLeft.collider != null)
        {
            return raycastLeft.collider.gameObject.GetComponent<MoveableEnemy>();
        }
        else if (raycastRight.collider != null)
        {
            return raycastRight.collider.gameObject.GetComponent<MoveableEnemy>();
        }
        else
        {
            return null;
        }
    }
}
