using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected string projectileName { get; set; }
    protected float Health { get; set; }
    protected float OffsetAttackX { get; set; }
    protected float OffsetAttackY { get; set; }
    protected float DistanceAttack { get; set; }
    protected float BuildingProhibitionDistance { get; set; }
    protected float DelayAttack { get; set; }
    protected float CurrentDelayAttack { get; set; }
    protected MoveableEnemy Enemy { get; set; }

    [Header("HP Bar")]
    [SerializeField] protected Transform hpBar;

    [Header("Abstract Layers")]
    [SerializeField] private LayerMask PlayerMask;
    [SerializeField] protected LayerMask EnemyMask;
    public abstract void Attack(MoveableEnemy Enemy);
    public abstract MoveableEnemy FindEnemy();

    public void DisablePlayerBuildSkill()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.left * BuildingProhibitionDistance);
        RaycastHit2D raycastRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.right, BuildingProhibitionDistance, PlayerMask);
        RaycastHit2D raycastLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.left, BuildingProhibitionDistance, PlayerMask);

        if (raycastLeft.collider != null || raycastRight.collider != null)
        {
            PlayerModel.CanBuild = false;
        }

    }

    public void DestroyTower(float health)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetDamage(Tower tower, float Damage)
    {
        tower.Health -= Damage;
    }


}
