using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowedTower : Tower
{
    private float _slowSpeedCoef;
    private MoveableEnemy _enemy;
    private List<MoveableEnemy> _listOfEnemies;
    void Start()
    {
        DistanceAttack = 8f;
        Health = 100f;
        BuildingProhibitionDistance = 2.8f;
        _slowSpeedCoef = 2.15f;
        OffsetProhibitionY = 1.2f;
        OffsetAttackY = 1.2f;
        _listOfEnemies = new List<MoveableEnemy>();
    }

    void FixedUpdate()
    {
        hpBar.localScale = new Vector2(hpBar.localScale.x, Health / 100);
        DestroyTower(Health);
        DisablePlayerBuildSkill();
        FindEnemies();
        Attack();
        UnregisterEnemies();
    }
    private void Attack()
    {
        if (_listOfEnemies.Count > 0)
        {
            for (int i = 0; i < _listOfEnemies.Count; ++i)
            {
                if (_listOfEnemies[i] != null && Vector2.Distance(transform.position, _listOfEnemies[i].transform.position) < DistanceAttack && _listOfEnemies[i].CurrentSpeed == _listOfEnemies[i].DefaultSpeed)
                {
                    _listOfEnemies[i].CurrentSpeed /= _slowSpeedCoef;
                }
            }
        }
    }

    private void FindEnemies()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.right * DistanceAttack);
        RaycastHit2D[] raycastLeft = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.left, DistanceAttack, EnemyMask);
        RaycastHit2D[] raycastRight = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + OffsetAttackY), Vector2.right, DistanceAttack, EnemyMask);

        if (raycastLeft.Length > 0)
        {
            for (int i = 0; i <  raycastLeft.Length; ++i)
            {
                _enemy = raycastLeft[i].collider.gameObject.GetComponent<MoveableEnemy>();
                if (!_listOfEnemies.Contains(_enemy)) {
                    _listOfEnemies.Add(_enemy);
                }
            }
        }

        if (raycastRight.Length > 0)
        {
            for (int i = 0; i < raycastRight.Length; ++i)
            {
                _enemy = raycastRight[i].collider.gameObject.GetComponent<MoveableEnemy>();
                if (!_listOfEnemies.Contains(_enemy))
                {
                    _listOfEnemies.Add(_enemy);
                }
            }
        }
    }

    private void UnregisterEnemies()
    {
        for (int i = 0; i < _listOfEnemies.Count; ++i)
        {
            if (_listOfEnemies[i] == null)
            {
                _listOfEnemies.Remove(_listOfEnemies[i]);
                continue;
            }

            if (Vector2.Distance(transform.position, _listOfEnemies[i].transform.position) >= DistanceAttack)
            {
                _listOfEnemies[i].CurrentSpeed = _listOfEnemies[i].DefaultSpeed;
                _listOfEnemies.Remove(_listOfEnemies[i]);
            }
        }
    }

    public override void Attack(MoveableEnemy Enemy)
    {
        return;
    }

    public override MoveableEnemy FindEnemy()
    {
        return null;
    }
}
