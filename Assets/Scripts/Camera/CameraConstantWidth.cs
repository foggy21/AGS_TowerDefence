using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Vector2 _defaultResolution = new Vector2(720, 1280);
    [Range(0f, 1f)] [SerializeField] private float _widthOrHeight = 0;

    private Camera _cameraComponent;
    private float _initialSize;
    private float _targetAspect;
    private float _initialFov;
    private float _horizontalFov = 120f;

    private void Start()
    {
        _cameraComponent = GetComponent<Camera>();
        _initialSize = _cameraComponent.orthographicSize;
        _targetAspect = _defaultResolution.x / _defaultResolution.y;
        _initialFov = _cameraComponent.fieldOfView;
        _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);
    }

    private void Update()
    {
        _defaultResolution = new Vector2(Screen.width, Screen.height);
        if (_cameraComponent.orthographic)
        {
            float constantWidthSize = _initialSize * (_targetAspect / _cameraComponent.aspect);
            _cameraComponent.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
        }
        else
        {
            float constantWidthFov = CalcVerticalFov(_horizontalFov, _cameraComponent.aspect);
            _cameraComponent.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
        }
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;
        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);
        return vFovInRads * Mathf.Rad2Deg;
    }
}
