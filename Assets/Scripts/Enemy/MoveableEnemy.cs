using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableEnemy : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;
    private EnemyModel _enemyModel;

    [Header("HP Bar")]
    [SerializeField] private Transform hpBar;
    [Header("Damage")]
    [SerializeField] private float damage;
    [Header("Money")]
    [SerializeField] private GameObject money;
    public Color DefaultColor { get; set; }
    public float CurrentHealth { get; set; }
    public float DefaultSpeed => _enemyModel.Speed;
    public float CurrentSpeed { get; set; }
    public bool CanMove { get; set; }
    private void Start()
    {
        _enemyModel = new EnemyModel();
        _enemyModel.LayerChecker = transform.GetChild(0);
        _enemyModel.Health = 100f;
        _enemyModel.Sprite = GetComponentInChildren<SpriteRenderer>();
        CanMove = true;
        CurrentSpeed = DefaultSpeed;
        DefaultColor = _enemyModel.Sprite.color;
        CurrentHealth = _enemyModel.Health;
        _enemyModel.VisitedPoints = new List<GameObject>();
        _enemyModel.MovePoints = _enemyView.GetMovePoints();
        _enemyModel.CurrentMovePoint = _enemyView.FindClosedMovePoint(_enemyModel.MovePoints, _enemyModel.VisitedPoints, _enemyModel.DistanceLimit);
        _enemyModel._changeMovePoint = false;
    }
    private void FixedUpdate()
    {
        hpBar.localScale = new Vector2(hpBar.localScale.x, CurrentHealth / 100);
        if (CurrentHealth <= 0)
        {
            GlobalEventManager.DecrementCountEnemies();
            Instantiate(money, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (_enemyModel.CurrentMovePoint == null)
        {
            Debug.Log("BUG IN ENEMY " + gameObject.transform.position);
        }
        if (!_enemyModel._changeMovePoint && CanMove)
        {
            _enemyView.MoveToPoint(_enemyModel.CurrentMovePoint, _enemyModel.Rigidbody, CurrentSpeed, ref _enemyModel._changeMovePoint, _enemyModel.LayerChecker, _enemyModel.CircleRadiusCheckingLayer);
            if (transform.position.x < _enemyModel.CurrentMovePoint.position.x)
            {
                _enemyModel.Sprite.flipX = true;
            }
            else
            {
                _enemyModel.Sprite.flipX = false;
            }
        }
        else if (_enemyView.CheckGround(_enemyModel.LayerChecker, _enemyModel.CircleRadiusCheckingLayer) == true && _enemyModel._changeMovePoint)
        {
            _enemyModel.CurrentMovePoint = _enemyView.FindClosedMovePoint(_enemyModel.MovePoints, _enemyModel.VisitedPoints, _enemyModel.DistanceLimit);
            _enemyModel._changeMovePoint = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Crystal")
        {
            GlobalEventManager.DecrementCountEnemies();
            Destroy(gameObject);
        }

        if (collision.tag == "Tower")
        {
            Tower tower = collision.GetComponent<Tower>();
            tower.GetDamage(tower, damage);
        }
    }
}
