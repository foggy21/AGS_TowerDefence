using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    private float _speed = 2.45f;
    private float _jumpForce = 10f;
    private float _distanceLimit = 9f; //Ограничение расстояние для поиска ближайшей точки.
    private float _circleRadiusCheckingLayer = 0.6f;
    public bool _changeMovePoint;
    public float Speed => _speed;
    public float DistanceLimit => _distanceLimit;
    public float JumpForce => _jumpForce;
    public float CircleRadiusCheckingLayer => _circleRadiusCheckingLayer;
    public float Health { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public Transform CurrentMovePoint { get; set; }
    public Transform LayerChecker { get; set; }
    public GameObject[] MovePoints { get; set; }
    public List<GameObject> VisitedPoints { get; set; }
    public SpriteRenderer Sprite { get; set; }
}
