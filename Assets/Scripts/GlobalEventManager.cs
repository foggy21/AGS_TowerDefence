using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GlobalEventManager
{
    public static UnityEvent OnCloseTowerMenu = new UnityEvent();
    public static UnityEvent<GameObject> OnSetEnemy = new UnityEvent<GameObject>();

    public static void CloseTowerMenu()
    {
        OnCloseTowerMenu.Invoke();
    }

    public static void SetEnemy(GameObject enemy)
    {
        OnSetEnemy.Invoke(enemy);
    }
}
