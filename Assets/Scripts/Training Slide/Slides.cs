using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slides : MonoBehaviour
{
    [SerializeField] private GameObject[] _slides;
    public static bool InTraining;
    private int countOfSlide = 0;
    void Start()
    {
        InTraining = true;
        Time.timeScale = 0;
        _slides[countOfSlide].SetActive(true);
    }

    public void ShowNextSlide()
    {
        if (countOfSlide == 2)
        {
            _slides[countOfSlide].SetActive(false);
            gameObject.SetActive(false);
            InTraining = false;
            Time.timeScale = 1;
        } 
        else
        {
            _slides[countOfSlide].SetActive(false);
            countOfSlide++;
            _slides[countOfSlide].SetActive(true);
        }
    }
}
