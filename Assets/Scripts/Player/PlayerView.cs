using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerModel _playerModel;

    [Header("Layers")]
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Ladder;

    private void Awake()
    {
        _playerModel = new PlayerModel
        {
            Rigidbody = GetComponent<Rigidbody2D>(),
            LayerChecker = transform.GetChild(0),
        };
        PlayerModel.CanMove = true;
    }

    public void Move()
    {
        if ((_playerModel.HorizontalInput > 0 || _playerModel.HorizontalInput < 0))
        {
            _playerModel.Rigidbody.velocity = new Vector2(_playerModel.HorizontalInput * _playerModel.Speed, _playerModel.Rigidbody.velocity.y);
        }
            
    }

    public void Jump()
    {
        if (CheckGround(_playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer))
        {
            _playerModel.Rigidbody.AddForce(_playerModel.JumpForce * _playerModel.JumpInput * Vector2.up, ForceMode2D.Impulse);
        }
    }

    public void Climb()
    {
        if (CheckLadder(_playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer))
        {
            if (_playerModel.VerticalInput > 0 || _playerModel.VerticalInput < 0)
                _playerModel.Rigidbody.velocity = new Vector2(_playerModel.Rigidbody.velocity.x, _playerModel.VerticalInput * _playerModel.Speed);
        }
    }

    public void FixedPosition()
    {
        _playerModel.Rigidbody.velocity = Vector2.zero;
    }

    private bool CheckGround(Transform groundChecker, float circleRadiusCheckingGround)
    {
        return Physics2D.OverlapCircle(groundChecker.position, circleRadiusCheckingGround, Ground);
    }

    private bool CheckLadder(Transform ladderChecker, float circleRadiusCheckingLadder)
    {
        return Physics2D.OverlapCircle(ladderChecker.position, circleRadiusCheckingLadder, Ladder);
    }
}
