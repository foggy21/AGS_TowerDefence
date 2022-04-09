using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerModel
{
    private float _circleRadiusCheckingLayer = 0.3f;
    private float _speed = 5;
    private float _jumpForce = 5;
    public static bool CanMove { get; set; }
    public float CircleRadiusCheckingLayer => _circleRadiusCheckingLayer;
    public float HorizontalInput => Input.GetAxis("Horizontal");
    public float VerticalInput => Input.GetAxis("Vertical");
    public float JumpInput => Input.GetAxis("Jump");
    public float JumpForce => _jumpForce;
    public float Speed => _speed;

    public Rigidbody2D Rigidbody { get; set; }
    public Transform LayerChecker { get; set; }
}
