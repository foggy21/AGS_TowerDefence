using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zap : Projectile
{
    [SerializeField] private LayerMask EnemyMask;

    private Zap _nextZap;
    public float DistanceAttack { get; set; }
    public int CountOfZaps { get; set; }
    public int LimitsOfZaps { get; set; }

    private void Awake()
    {
        GlobalEventManager.OnSetNewStats.AddListener(SetNewStats);
    }

    private void Start()
    {
        Enemy = transform.parent.GetComponent<MoveableEnemy>();
        Damage = 35f;
        StartCoroutine(Electrify(Enemy));
    }
    
    private void ZapNextEnemy(MoveableEnemy enemy, float distanceAttack, int countOfZaps)
    {
        GameObject nextEnemy = FindNextEnemy(enemy, distanceAttack);
        if (nextEnemy != null)
        {
            _nextZap = Resources.Load<Zap>("Zap");
            Instantiate(_nextZap, nextEnemy.transform);
            _nextZap.CountOfZaps = countOfZaps + 1;

        }
    }

    private GameObject FindNextEnemy(MoveableEnemy enemy, float distanceAttack)
    {
        Debug.DrawRay(new Vector2(enemy.transform.position.x + 1.1f, transform.position.y), Vector2.right * distanceAttack);
        Debug.DrawRay(new Vector2(enemy.transform.position.x - 1.1f, transform.position.y), Vector2.left * distanceAttack);
        RaycastHit2D raycastLeft = Physics2D.Raycast(new Vector2(enemy.transform.position.x - 1.1f, transform.position.y), Vector2.left, distanceAttack, EnemyMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(new Vector2(enemy.transform.position.x + 1.1f, transform.position.y), Vector2.right, distanceAttack, EnemyMask);
       
        if (raycastLeft.collider != null)
        {
            return raycastLeft.collider.gameObject;
        }
        else if (raycastRight.collider != null)
        {
            return raycastRight.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    IEnumerator Electrify(MoveableEnemy enemy)
    {
        enemy.CanMove = false;
        enemy.CurrentHealth -= Damage;
        SpriteRenderer sprite = enemy.GetComponentInChildren<SpriteRenderer>();
        sprite.color = Color.blue;
        yield return new WaitForSeconds(0.3f);
        if (Enemy)
        {
            sprite.color = enemy.DefaultColor;
            enemy.CanMove = true;
            if (CountOfZaps < LimitsOfZaps)
            {
                ZapNextEnemy(Enemy, DistanceAttack, CountOfZaps);
            }
        }
        Destroy(gameObject);
    }

    public void SetNewStats(int countOfZaps, int limitOfZaps, float distanceOfAttack)
    {
        CountOfZaps = countOfZaps;
        LimitsOfZaps = limitOfZaps;
        DistanceAttack = distanceOfAttack;
    }
}
