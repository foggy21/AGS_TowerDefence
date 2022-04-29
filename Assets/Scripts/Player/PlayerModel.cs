using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerModel
{
    private float _circleRadiusCheckingLayer = 0.3f;
    private float _speed = 5f;
    private float _jumpForce = 11.5f;
    private float _timeInSlowMove = 10f;

    public int PlayerLayer { get; set; }
    public int GroundLayer { get; set; }
    public float CurrentTimeInSlowMove { get; set; }
    public bool SlowMove { get; set; }
    public static int Money { get; set; }
    public static bool CanMove { get; set; }
    public static bool CanBuild { get; set; }

    public float TimeInSlowMove => _timeInSlowMove;
    public float CircleRadiusCheckingLayer => _circleRadiusCheckingLayer;
    public float HorizontalInput => Input.GetAxis("Horizontal");
    public float VerticalInput => Input.GetAxis("Vertical");
    public float JumpInput => Input.GetAxis("Jump");
    public float JumpForce => _jumpForce;
    public float Speed => _speed;

    public Collider2D PlayerCollider { get; set; }

    public Text WarningField { get; set; }
    public Text WarningFieldMoney { get; set; }
    public Text TextTimeInSlowMove { get; set; }
    public Text MoneyField { get; set; }
    public Animator Animator { get; set; }
    public SpriteRenderer Sprite { get; set; }
    public Rigidbody2D Rigidbody { get; set; }
    public Transform LayerChecker { get; set; }
}
