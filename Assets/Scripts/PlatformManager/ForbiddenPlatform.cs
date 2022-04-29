using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForbiddenPlatform : MonoBehaviour
{
    public bool isForbidden;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isForbidden && collision.collider.tag == "Player")
        {
            Debug.Log("HERE");
            PlayerModel.CanBuild = false;
        }
        else
        {
            PlayerModel.CanBuild = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isForbidden && collision.collider.tag == "Player")
        {
            Debug.Log("Exit");
            PlayerModel.CanBuild = true;
        }
    }
}
