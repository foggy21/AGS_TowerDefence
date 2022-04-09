using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    private PlayerModel _playerModel = new PlayerModel();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!transform.GetChild(0).gameObject.activeSelf)
            {
                ShowTowerMenu();
            } else
            {
                CloseTowerMenu();
            }
        }
    }

    private void ShowTowerMenu()
    {
        PlayerModel.CanMove = false;
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    } 

    private void CloseTowerMenu()
    {
        PlayerModel.CanMove = true;
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
