using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManagment: MonoBehaviour
{
    private void Awake()
    {
        GlobalEventManager.OnCloseTowerMenu.AddListener(CloseTowerMenu);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !CameraMovement._isExpanded)
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

    public void CloseTowerMenu()
    {
        PlayerModel.CanMove = true;
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
