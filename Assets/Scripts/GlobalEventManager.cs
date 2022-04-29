using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent OnCloseTowerMenu = new UnityEvent();
    public static UnityEvent OnDecrementCountEnemies = new UnityEvent();
    public static UnityEvent<MoveableEnemy> OnSetEnemy = new UnityEvent<MoveableEnemy>();
    public static UnityEvent<MoveableEnemy, float> OnGetDamage = new UnityEvent<MoveableEnemy, float>();
    public static UnityEvent<int, int, float> OnSetNewStats = new UnityEvent<int, int, float>();

    public static void CloseTowerMenu()
    {
        OnCloseTowerMenu.Invoke();
    }

    public static void DecrementCountEnemies()
    {
        OnDecrementCountEnemies.Invoke();
    }

    public static void SetEnemy(MoveableEnemy enemy)
    {
        OnSetEnemy.Invoke(enemy);
    }

    public static void GetDamage(MoveableEnemy enemy, float damage)
    {
        OnGetDamage.Invoke(enemy, damage);
    }

    public static void SetNewStats(int countOfStat, int limitOfStat, float distanceOfStat)
    {
        OnSetNewStats.Invoke(countOfStat, limitOfStat, distanceOfStat);
    }

    private void Awake()
    {
        if (!MenuManager.Restarted) OnGetDamage.AddListener(EnemyView.GetDamage);
        if (!MenuManager.Restarted) OnDecrementCountEnemies.AddListener(WaveManager.DecrementCountEnemies);
    }
}
