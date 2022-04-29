using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] _platformTamplates;
    private Animator[] _animators;
    private int countOfAnimators;

    private void Start()
    {
        _animators = GetComponentsInChildren<Animator>();
        countOfAnimators = _animators.Length;
    }

    public IEnumerator ChangePlatform()
    {
        for (int i = 0; i < countOfAnimators; ++i)
        {
           _animators[i].SetBool("Destroy", true);
        }
        yield return new WaitForSeconds(2.3f);
        for (int i = 0; i < countOfAnimators; ++i)
        {
            _animators[i].SetBool("Destroy", false);
        }
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            Instantiate(_platformTamplates[0], transform);
        }
    }
}
