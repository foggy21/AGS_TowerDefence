using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent OnCloseTowerMenu = new UnityEvent();
    public static UnityEvent<GameObject> OnSetEnemy = new UnityEvent<GameObject>();
    public static UnityEvent<MoveableEnemy, int> OnGetDamage = new UnityEvent<MoveableEnemy, int>();

    public static void CloseTowerMenu()
    {
        OnCloseTowerMenu.Invoke();
    }

    public static void SetEnemy(GameObject enemy)
    {
        OnSetEnemy.Invoke(enemy);
    }

    public static void GetDamage(MoveableEnemy health, int damage)
    {
        OnGetDamage.Invoke(health, damage);
    }

    private void Awake()
    {
        OnGetDamage.AddListener(EnemyView.GetDamage);
        DontDestroyOnLoad(gameObject);
    }
}
