using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _dumping = 1.5f;
    private bool _isLeft;
    private Vector3 _offset = new Vector2(2f, 1f);
    private Vector3 _targetPos;
    private Vector3 _currentPos;
    private Transform _playerTransform;
    private int _currentX;
    private int _lastX;

    [SerializeField] private float rightLimit;
    [SerializeField] private float leftLimit;
    [SerializeField] private float upperLimit;
    [SerializeField] private float bottomLimit;

    private void Start()
    {
        _offset = new Vector3(Mathf.Abs(_offset.x), _offset.y);
        FindPlayer(_isLeft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _lastX = Mathf.RoundToInt(_playerTransform.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(_playerTransform.position.x - _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_playerTransform.position.x + _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (_playerTransform)
        {
            _currentX = Mathf.RoundToInt(_playerTransform.position.x);
            if (_currentX > _lastX)
            {
                _isLeft = false;
            } 
            else if (_currentX < _lastX)
            {
                _isLeft = true;
            }
            _lastX = Mathf.RoundToInt(_playerTransform.position.x);

            if (_isLeft)
            {
                _targetPos = new Vector3(_playerTransform.position.x - _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
            }
            else
            {
                _targetPos = new Vector3(_playerTransform.position.x + _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
            }

            _currentPos = Vector3.Lerp(transform.position, _targetPos, _dumping * Time.deltaTime);
            transform.position = _currentPos;
        }

        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            transform.position.z
            );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(rightLimit, upperLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, upperLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, upperLimit), new Vector2(leftLimit, bottomLimit));
    }
}
