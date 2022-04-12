using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected float OffsetAttackX { get; set; }
    protected float OffsetAttackY { get; set; }
    protected float DistanceAttack { get; set; }
    protected float DelayAttack { get; set; }
    protected float CurrentDelayAttack { get; set; }
    protected List<GameObject> ListOfEnemies { get; set; }
    protected GameObject Enemy { get; set; }
    protected GameObject TargetEnemy { get; set; }

    [Header("Layers")]
    [SerializeField] private LayerMask EnemyMask;
    public abstract void Attack(GameObject Enemy, string ProjectileName);

    public GameObject FindEnemy()
    {
        RaycastHit2D raycastRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.right, DistanceAttack, EnemyMask);
        RaycastHit2D raycastLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.left, DistanceAttack, EnemyMask);

        if (raycastLeft.collider != null)
        {
            return raycastLeft.collider.gameObject;
        }
        if (raycastRight.collider != null)
        {
            return raycastRight.collider.gameObject;
        }

        return null;
    }

    public GameObject ChooseEnemy(List<GameObject> listOfEnemies)
    {
        TargetEnemy = listOfEnemies[0];
        return TargetEnemy;
    }

    public List<GameObject> RegisterNewEnemy(List<GameObject> listOfEnemies, GameObject newEnemy)
    {
        listOfEnemies.Add(newEnemy);
        return listOfEnemies;
    }

    public List<GameObject> UnregisterEnemy(List<GameObject> listOfEnemies, GameObject enemy)
    {
        listOfEnemies.Remove(enemy);
        return listOfEnemies;
    }

}
