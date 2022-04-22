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
        Damage = 1;
        StartCoroutine(Electrify(Enemy));
        
    }
    
    private void ZapNextEnemy(MoveableEnemy enemy, float distanceAttack, int countOfZaps)
    {
        if (FindNextEnemy(enemy, distanceAttack) != null)
        {
            _nextZap = Resources.Load<Zap>("Zap");
            _nextZap.CountOfZaps = countOfZaps + 1;
            Instantiate(_nextZap, enemy.transform);
        }
    }

    private GameObject FindNextEnemy(MoveableEnemy enemy, float distanceAttack)
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(new Vector2(enemy.transform.position.x - 1.1f, enemy.transform.position.y), Vector2.left, distanceAttack, EnemyMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(new Vector2(enemy.transform.position.x + 1.1f, enemy.transform.position.y), Vector2.right, distanceAttack, EnemyMask);
       
        if (raycastLeft.collider != null && raycastLeft.collider.gameObject.GetComponent<MoveableEnemy>() != enemy)
        {
            return raycastLeft.collider.gameObject;
        }
        else if (raycastRight.collider != null && raycastRight.collider.gameObject.GetComponent<MoveableEnemy>() != enemy)
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
        Color blue = Color.blue;
        sprite.color = blue;
        yield return new WaitForSeconds(0.3f);
        if (Enemy)
        {
            Debug.Log("End Coroutine");
            sprite.color = enemy.DefaultColor;
            enemy.CanMove = true;
        }
        if (Enemy && CountOfZaps < LimitsOfZaps)
        {
            ZapNextEnemy(Enemy, DistanceAttack, CountOfZaps);
        }
        Destroy(gameObject);
    }

    public void SetNewStats(int countOfZaps, int limitOfZaps, float distanceOfAttack)
    {
        CountOfZaps = countOfZaps;
        LimitsOfZaps = limitOfZaps;
        DistanceAttack = distanceOfAttack/2;
    }
}
