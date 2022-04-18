using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask Ground;
    [SerializeField] private LayerMask Ladder;
    public GameObject[] GetMovePoints()
    {
        return GameObject.FindGameObjectsWithTag("Movepoint");
    }

    public Transform FindClosedMovePoint(GameObject[] movePoint, List<GameObject> visitedPoints, float distanceLimit)
    {
        Transform closedPoint = null;
        float minDistance = distanceLimit;
        for (int i = 0; i < movePoint.Length; ++i)
        {
            float distanceToPoint = Vector2.Distance(transform.position, movePoint[i].transform.position);
            if (distanceToPoint > distanceLimit)
            {
                continue;
            }

            if (distanceToPoint < minDistance)
            {
                if (visitedPoints.Contains(movePoint[i]))
                {
                    continue;
                }
                Debug.Log(movePoint[i]);
                minDistance = distanceToPoint;
                closedPoint = movePoint[i].transform;
            }
        }

        visitedPoints.Add(closedPoint.gameObject);
        return closedPoint;
    }

    public void MoveToPoint(Transform point, Rigidbody2D rb, float speed, ref bool changePoint, Transform checkLayer, float circleRadiusCheckingLayer)
    {
        bool? atStair = null;
        if (atStair == null) atStair = CheckLadder(checkLayer, circleRadiusCheckingLayer);

        //Debug.Log(point);
        if (Mathf.Abs(point.position.y - transform.position.y) > 3f  && (bool)atStair)
        {
            ClimbStair(point, rb, speed, ref changePoint);
        }
        else
        {
            WalkOnPlatform(point, speed, ref changePoint, checkLayer, circleRadiusCheckingLayer);
        }
    }

    private void ClimbStair(Transform point, Rigidbody2D rb, float speed, ref bool changePoint)
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, point.position) <= 0.1f)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            ChangeNewPoint(ref changePoint);
        }
    }

    private void WalkOnPlatform(Transform point, float speed, ref bool changePoint, Transform checkLayer, float circleRadiusCheckingLayer)
    {
        bool onGround = CheckGround(checkLayer, circleRadiusCheckingLayer);
        if (transform.position.x > point.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        } 
        else if (transform.position.x < point.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, point.position) <= 0.5f)
        {
            ChangeNewPoint(ref changePoint);
        }
    }

    private void ChangeNewPoint(ref bool changePoint)
    {
        changePoint = true;
    }

    public bool CheckGround(Transform groundChecker, float circleRadiusCheckingGround)
    {
        return Physics2D.OverlapCircle(groundChecker.position, circleRadiusCheckingGround, Ground);
    }

    private bool CheckLadder(Transform ladderChecker, float circleRadiusCheckingLadder)
    {
        return Physics2D.OverlapCircle(ladderChecker.position, circleRadiusCheckingLadder, Ladder);
    }
}
