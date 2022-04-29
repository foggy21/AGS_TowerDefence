using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Ladder;
    [SerializeField] private LayerMask MottledGround;

    private void Awake()
    {
        PlayerModel.CanMove = true;
    }

    public void Move(Rigidbody2D rigidbody2D, Animator animator, float HorizontalInput, float speed)
    {
        if (HorizontalInput > 0 || HorizontalInput < 0)
        {
            animator.SetBool("CanMove", true);
            rigidbody2D.velocity = new Vector2(HorizontalInput * speed, rigidbody2D.velocity.y);
        }
        else
        {
            animator.SetBool("CanMove", false);
        }
    }

    public void MoveSlow(Rigidbody2D rigidbody2D, Animator animator, float HorizontalInput, float speed)
    {
        if (HorizontalInput > 0 || HorizontalInput < 0)
        {
            animator.SetBool("CanMoveSlow", true);
            rigidbody2D.velocity = new Vector2(HorizontalInput * (speed / 2), rigidbody2D.velocity.y);
        }
        else
        {
            animator.SetBool("CanMoveSlow", false);
        }
    }

    public void Jump(Rigidbody2D rigidbody2D, float jumpInput, float jumpForce, Transform layerChecker, float circleRadiusCheckingLayer)
    {
        if (CheckGround(layerChecker, circleRadiusCheckingLayer))
        {
            rigidbody2D.AddForce(jumpForce * jumpInput * Vector2.up, ForceMode2D.Impulse);
        }
    }

    public void Climb(Rigidbody2D rigidbody2D, Animator animator, float verticalInput, float speed, Transform layerChecker, float circleRadiusCheckingLayer, int playerLayer, int groundLayer)
    {
        
        if (CheckLadder(layerChecker, circleRadiusCheckingLayer))
        {
            if (verticalInput > 0 || verticalInput < 0)
            {
                animator.SetBool("CanClimb", true);
                Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalInput * speed);
            }
        }
        else
        {
            animator.SetBool("CanClimb", false);
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
        }
    }

    public IEnumerator ShowWarning(Text warning)
    {
        warning.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.9f);
        warning.gameObject.SetActive(false);
    }


    
    public void FixedPosition(Rigidbody2D rigidbody2D)
    {
        rigidbody2D.velocity = Vector2.zero;
    }

    private bool CheckGround(Transform groundChecker, float circleRadiusCheckingGround)
    {
        return Physics2D.OverlapCircle(groundChecker.position, circleRadiusCheckingGround, Ground) || Physics2D.OverlapCircle(groundChecker.position, circleRadiusCheckingGround, MottledGround);
    }

    public bool CheckGroundForBuild(Transform groundChecker, float circleRadiusCheckingGround)
    {
        return Physics2D.OverlapCircle(groundChecker.position, circleRadiusCheckingGround, Ground);
    }

    public bool CheckLadder(Transform ladderChecker, float circleRadiusCheckingLadder)
    {
        return Physics2D.OverlapCircle(ladderChecker.position, circleRadiusCheckingLadder, Ladder);
    }
}
