using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public void Move(Rigidbody2D rigidbody, float moveInput, float speed)
    {
        rigidbody.velocity = new Vector2(moveInput * speed, rigidbody.velocity.y);
    }

    public void Jump(Rigidbody2D rigidbody, float moveInput, float jumpForce, Transform groundChecker, float circleRadiusCheckingGround, LayerMask Ground)
    {
        if (CheckGround(groundChecker, circleRadiusCheckingGround, Ground))
        {
            rigidbody.AddForce(Vector2.up * moveInput * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Climb(Rigidbody2D rigidbody, float moveInput, float speed, Transform ladderChecker, float circleRadiusCheckingLadder, LayerMask Ladder)
    {
        if (CheckLadder(ladderChecker, circleRadiusCheckingLadder, Ladder))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, moveInput * speed);
        }
    } 

    private bool CheckGround(Transform groundChecker, float circleRadiusCheckingGround, LayerMask Ground)
    {
        return Physics2D.OverlapCircle(groundChecker.position, circleRadiusCheckingGround, Ground);   
    }

    private bool CheckLadder(Transform ladderChecker, float circleRadiusCheckingLadder, LayerMask Ladder)
    {
        return Physics2D.OverlapCircle(ladderChecker.position, circleRadiusCheckingLadder, Ladder);
    }
}
