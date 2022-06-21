using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static bool Restarted;
    public static bool Paused;
    private AudioSource _sound;
    [SerializeField] private GameObject _menu;
    [SerializeField] private Image _soundSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private Sprite _soundOnSprite;

    private void Start()
    {
        _sound = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_menu.activeSelf && !Slides.InTraining)
            {
                Time.timeScale = 0;
                Paused = true;
                _menu.SetActive(true);
            }
            else if (_menu.activeSelf)
            {
                Time.timeScale = 1;
                Paused = false;
                _menu.SetActive(false);
            }
        }
    }

    public void RastartLevel()
    {
        if (!Restarted) Restarted = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        if (!_menu.activeSelf && !Slides.InTraining)
        {
            Time.timeScale = 0;
            Paused = true;
            _menu.SetActive(true);
        }
    }
    public void SoundSwitch()
    {
        if (_sound.volume == 1)
        {
            _sound.volume = 0;
            _soundSprite.sprite = _soundOffSprite;
        }
        else
        {
            _sound.volume = 1;
            _soundSprite.sprite = _soundOnSprite;
        }
    }

    public void Href()
    {
        Application.OpenURL("https://fog-studio.itch.io/souls-of-dungeon");
    }
}
