using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    private PlayerModel _playerModel;

    private void Start()
    {
        _playerModel = new PlayerModel();
        _playerModel.LayerChecker = transform.GetChild(0);
        _playerModel.Rigidbody = GetComponent<Rigidbody2D>();
        _playerModel.Animator = GetComponent<Animator>();
        _playerModel.Sprite = GetComponentInChildren<SpriteRenderer>();
        _playerModel.PlayerLayer = LayerMask.NameToLayer("Player");
        _playerModel.GroundLayer = LayerMask.NameToLayer("Ground");
        Debug.Log(_playerModel.PlayerLayer + " " + _playerModel.GroundLayer);
        PlayerModel.CanBuild = true;
    }

    private void FixedUpdate()
    {
        if (PlayerModel.CanMove)
        {
            _playerView.Move(_playerModel.Rigidbody, _playerModel.HorizontalInput, _playerModel.Speed);
            _playerView.Climb(_playerModel.Rigidbody, _playerModel.VerticalInput, _playerModel.Speed, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer, _playerModel.PlayerLayer, _playerModel.GroundLayer);

            if (_playerModel.Rigidbody.velocity.y <= 0)
            {
                _playerView.Jump(_playerModel.Rigidbody, _playerModel.JumpInput, _playerModel.JumpForce, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer);
            }

            if (_playerModel.HorizontalInput > 0 || _playerModel.HorizontalInput < 0)
            {
                _playerModel.Animator.SetBool("CanMove", true);
                if (_playerModel.HorizontalInput < 0)
                {
                    _playerModel.Sprite.flipX = true;
                }
                else
                {
                    _playerModel.Sprite.flipX = false;
                }
            }
            else
            {
                _playerModel.Animator.SetBool("CanMove", false);
            }
        } 
        else
        {
            _playerView.FixedPosition(_playerModel.Rigidbody);
        }

        if (!_playerView.CheckGround(_playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer))
        {
            PlayerModel.CanBuild = false;
        }
        else
        {
            PlayerModel.CanBuild = true;
        }
    }

    public void BuildTower(string Tower)
    {
        if (PlayerModel.CanBuild)
        {
            Instantiate(Resources.Load<GameObject>(Tower), new Vector2(transform.position.x, transform.position.y - 1.32f), Quaternion.identity);
        }
        GlobalEventManager.CloseTowerMenu();
    }
}
