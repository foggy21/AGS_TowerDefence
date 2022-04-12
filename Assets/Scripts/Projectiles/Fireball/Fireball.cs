using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    private const float _speed = 8f;

    private void Awake()
    {
        Damage = 5f;
        GlobalEventManager.OnGetEnemy.AddListener(GetEnemy);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Enemy.transform.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
