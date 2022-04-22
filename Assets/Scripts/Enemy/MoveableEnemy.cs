using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableEnemy : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;
    private EnemyModel _enemyModel;
    
    
    public Color DefaultColor { get; set; }
    public int CurrentHealth { get; set; }
    public bool CanMove { get; set; }
    private void Start()
    {
        _enemyModel = new EnemyModel();
        _enemyModel.LayerChecker = transform.GetChild(0);
        _enemyModel.Health = 2;
        _enemyModel.Sprite = GetComponentInChildren<SpriteRenderer>();
        CanMove = true;
        DefaultColor = _enemyModel.Sprite.color;
        CurrentHealth = _enemyModel.Health;
        _enemyModel.VisitedPoints = new List<GameObject>();
        _enemyModel.MovePoints = _enemyView.GetMovePoints();
        _enemyModel.CurrentMovePoint = _enemyView.FindClosedMovePoint(_enemyModel.MovePoints, _enemyModel.VisitedPoints, _enemyModel.DistanceLimit);
        _enemyModel._changeMovePoint = false;
    }
    private void FixedUpdate()
    {
        if (CurrentHealth <= 0)
        {
            GlobalEventManager.DecrementCountEnemies();
            Destroy(gameObject);
        }
        if (_enemyModel.CurrentMovePoint == null)
        {
            Debug.Log("BUG IN ENEMY " + gameObject.transform.position);
        }
        if (!_enemyModel._changeMovePoint && CanMove)
        {
            _enemyView.MoveToPoint(_enemyModel.CurrentMovePoint, _enemyModel.Rigidbody, _enemyModel.Speed, ref _enemyModel._changeMovePoint, _enemyModel.LayerChecker, _enemyModel.CircleRadiusCheckingLayer);
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
    }
}
