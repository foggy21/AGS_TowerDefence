using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent OnCloseTowerMenu = new UnityEvent();
    public static UnityEvent<GameObject> OnGetEnemy = new UnityEvent<GameObject>();
    public static UnityEvent<Projectile, float> OnGetDamage = new UnityEvent<Projectile, float>();

    public static void CloseTowerMenu()
    {
        OnCloseTowerMenu.Invoke();
    }

    public static void GetEnemy(GameObject enemy)
    {
        OnGetEnemy.Invoke(enemy);
    }

    public static void GetDamage(Projectile projectitle, float damage)
    {
        OnGetDamage.Invoke(projectitle, damage);
    }
}
