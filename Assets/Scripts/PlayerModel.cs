using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float _circleRadiusCheckingLayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    public LayerMask Ground { get; set; }
    public LayerMask Ladder { get; set; }

    public float CircleRadiusCheckingLayer => _circleRadiusCheckingLayer;
    public float HorizontalInput => Input.GetAxis("Horizontal");
    public float VerticalInput => Input.GetAxis("Vertical");
    public float JumpInput => Input.GetAxis("Jump");
    public float JumpForce => _jumpForce;
    public float Speed => _speed;

    public Rigidbody2D Rigidbody => GetComponent<Rigidbody2D>();
    public Transform LayerChecker => GetComponentInChildren<Transform>();
}
