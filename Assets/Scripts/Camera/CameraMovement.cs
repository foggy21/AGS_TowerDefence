using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera currentCamera;
    private float _dumping = 1.5f;
    private float _defaultSize = 9f;
    private float _expandedSize = 19f;
    private Vector3 _expandedCoord = new Vector3(0f, 12.75f, -10f);
    private bool _isExpanded;
    private bool _isLeft;
    private Vector3 _offset = new Vector2(2f, 1f);
    private Vector3 _targetPos;
    private Vector3 _currentPos;
    private Vector3 _lastMouse = new Vector3(0, 0, -10f);
    private Transform _playerTransform;
    private int _currentX;
    private int _lastX;

    [SerializeField] private float rightLimit;
    [SerializeField] private float leftLimit;
    [SerializeField] private float upperLimit;
    [SerializeField] private float bottomLimit;

    private void Start()
    {
        currentCamera = gameObject.GetComponent<Camera>();
        _offset = new Vector3(Mathf.Abs(_offset.x), _offset.y);
        _isExpanded = false;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !_isExpanded)
        {
            leftLimit /= 2;
            rightLimit /= 2;
            _isExpanded = true;
            PlayerModel.CanMove = false;
            Camera.main.orthographicSize = _expandedSize;
            Camera.main.transform.position = _expandedCoord;
            
        } 
        else if (Input.GetKeyDown(KeyCode.M) && _isExpanded)
        {
            leftLimit *= 2;
            rightLimit *= 2;
            _isExpanded = false;
            PlayerModel.CanMove = true;
            Camera.main.orthographicSize = _defaultSize;
        }

        if (!_isExpanded)
        {
            if (_playerTransform)
            {
                //Слежение за игроком
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
        }
        else
        {
            ExpandCamera();
        }
        

        transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            transform.position.z
            );
    }


    private void ExpandCamera()
    {
        _targetPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
        _currentPos = Vector3.Lerp(transform.position, _targetPos, _dumping * Time.deltaTime);
        transform.position = _currentPos;
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
