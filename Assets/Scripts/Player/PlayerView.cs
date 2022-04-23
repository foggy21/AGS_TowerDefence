using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Ladder;

    private void Awake()
    {
        PlayerModel.CanMove = true;
    }

    public void Move(Rigidbody2D rigidbody2D, float HorizontalInput, float speed)
    {
        if (HorizontalInput > 0 || HorizontalInput < 0)
        {
            rigidbody2D.velocity = new Vector2(HorizontalInput * speed, rigidbody2D.velocity.y);
        }
    }

    public void Jump(Rigidbody2D rigidbody2D, float jumpInput, float jumpForce, Transform layerChecker, float circleRadiusCheckingLayer)
    {
        if (CheckGround(layerChecker, circleRadiusCheckingLayer))
        {
            rigidbody2D.AddForce(jumpForce * jumpInput * Vector2.up, ForceMode2D.Impulse);
        }
    }

    public void Climb(Rigidbody2D rigidbody2D, float verticalInput, float speed, Transform layerChecker, float circleRadiusCheckingLayer, int playerLayer, int groundLayer)
    {
        
        if (CheckLadder(layerChecker, circleRadiusCheckingLayer))
        {
            if (verticalInput > 0 || verticalInput < 0)
            {
                Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalInput * speed);
            }
                
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
        }
    }


    
    public void FixedPosition(Rigidbody2D rigidbody2D)
    {
        rigidbody2D.velocity = Vector2.zero;
    }

    public bool CheckGround(Transform groundChecker, float circleRadiusCheckingGround)
    {
        return Physics2D.OverlapCircle(groundChecker.position, circleRadiusCheckingGround, Ground);
    }

    public bool CheckLadder(Transform ladderChecker, float circleRadiusCheckingLadder)
    {
        return Physics2D.OverlapCircle(ladderChecker.position, circleRadiusCheckingLadder, Ladder);
    }
}
