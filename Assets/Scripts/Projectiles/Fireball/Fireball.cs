using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{ 
    private const float _speed = 8f;

    private void Awake()
    {
        GlobalEventManager.OnSetEnemy.AddListener(SetEnemy);
        Damage = 5f;
    }

    private void Start()
    {
        CurrentEnemy = Enemy;
    }

    private void FixedUpdate()
    {
        if (CurrentEnemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, CurrentEnemy.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
