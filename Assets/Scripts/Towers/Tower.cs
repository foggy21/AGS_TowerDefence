using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected string projectileName { get; set; }
    protected float OffsetAttackX { get; set; }
    protected float OffsetAttackY { get; set; }
    protected float RadiusAttack { get; set; }
    protected float BuildingProhibitionDistance { get; set; }
    protected float DelayAttack { get; set; }
    protected float CurrentDelayAttack { get; set; }
    protected GameObject Enemy { get; set; }

    [Header("Abstract Layers")]
    [SerializeField] private LayerMask PlayerMask;
    [SerializeField] protected LayerMask EnemyMask;
    public abstract void Attack(GameObject Enemy);

    public void DisablePlayerBuildSkill()
    {
        RaycastHit2D raycastRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.right, BuildingProhibitionDistance, PlayerMask);
        RaycastHit2D raycastLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.left, BuildingProhibitionDistance, PlayerMask);

        if (raycastLeft.collider != null || raycastRight.collider != null)
        {
            PlayerModel.CanBuild = false;
        }
    }
    public abstract GameObject FindEnemy();
}
