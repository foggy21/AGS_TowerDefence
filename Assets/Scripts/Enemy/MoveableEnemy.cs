using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableEnemy : MonoBehaviour
{
    [SerializeField] private EnemyView _enemyView;
    private EnemyModel _enemyModel;
    private void Start()
    {
        _enemyModel = new EnemyModel();
        _enemyModel.LayerChecker = transform.GetChild(0);
        _enemyModel.VisitedPoints = new List<GameObject>();
        _enemyModel.MovePoints = _enemyView.GetMovePoints();
        _enemyModel.CurrentMovePoint = _enemyView.FindClosedMovePoint(_enemyModel.MovePoints, _enemyModel.VisitedPoints, _enemyModel.DistanceLimit);
        _enemyModel._changeMovePoint = false;
        Debug.Log(_enemyModel.CurrentMovePoint.position);
    }

    private void FixedUpdate()
    {
        if (_enemyModel.CurrentMovePoint == null)
        {
            Debug.Log("SUKA BLYAT " + gameObject.transform.position);
        }
        if (!_enemyModel._changeMovePoint)
        {
            _enemyView.MoveToPoint(_enemyModel.CurrentMovePoint, _enemyModel.Rigidbody, _enemyModel.Speed, ref _enemyModel._changeMovePoint, _enemyModel.LayerChecker, _enemyModel.CircleRadiusCheckingLayer);
        }
        else if (_enemyView.CheckGround(_enemyModel.LayerChecker, _enemyModel.CircleRadiusCheckingLayer) == true)
        {
            _enemyModel.CurrentMovePoint = _enemyView.FindClosedMovePoint(_enemyModel.MovePoints, _enemyModel.VisitedPoints, _enemyModel.DistanceLimit);
            _enemyModel._changeMovePoint = false;
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Crystal")
        {
            Destroy(gameObject);
        }
    }



}
