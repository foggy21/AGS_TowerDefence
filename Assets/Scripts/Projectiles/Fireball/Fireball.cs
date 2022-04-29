using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{ 
    private const float _speed = 8f;
    private Animator _animator;

    private void Awake()
    {
        GlobalEventManager.OnSetEnemy.AddListener(SetEnemy);
        _animator = GetComponent<Animator>();
        Damage = 25f;
    }

    private void Start()
    {
        CurrentEnemy = Enemy;
    }

    private void FixedUpdate()
    {
        if (CurrentEnemy != null)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(CurrentEnemy.transform.position.y - transform.position.y, CurrentEnemy.transform.position.x - transform.position.x) * Mathf.Rad2Deg);
            transform.position = Vector2.MoveTowards(transform.position, CurrentEnemy.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            StartCoroutine(Explosion());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            MoveableEnemy enemy = collision.GetComponent<MoveableEnemy>();
            GlobalEventManager.GetDamage(enemy, Damage);
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {
        _animator.SetBool("Explosion", true);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
