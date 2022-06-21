using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private Sprite[] _SpritesCrystal;
    [SerializeField] private GameObject[] _fails;
    [SerializeField] private GameObject GameOverMenu;

    private SpriteRenderer _sprite;
    private int _damage = 0;
    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _sprite.sprite = _SpritesCrystal[_damage];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            _damage += 1;
            _fails[_damage - 1].SetActive(false);
            if (_damage == 3)
            {
                GameOverMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                _sprite.sprite = _SpritesCrystal[_damage];
            }
        }
    }
}
