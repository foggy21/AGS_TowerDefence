using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    private PlayerModel _playerModel;

    private void Start()
    {
        _playerModel = new PlayerModel();
        _playerModel.SlowMove = false;
        _playerModel.CurrentTimeInSlowMove = 0;
        _playerModel.LayerChecker = transform.GetChild(0);
        _playerModel.TextTimeInSlowMove = GameObject.FindGameObjectWithTag("TextOfTimeInSlowMove").GetComponent<Text>();
        _playerModel.WarningField = GameObject.FindGameObjectWithTag("WarningField").GetComponent<Text>();
        _playerModel.WarningFieldMoney = GameObject.FindGameObjectWithTag("WarningFieldMoney").GetComponent<Text>();
        _playerModel.MoneyField = GameObject.FindGameObjectWithTag("CountOfMoney").GetComponent<Text>();
        _playerModel.PlayerCollider = GetComponent<Collider2D>();
        _playerModel.Rigidbody = GetComponent<Rigidbody2D>();
        _playerModel.Animator = GetComponent<Animator>();
        _playerModel.Sprite = GetComponentInChildren<SpriteRenderer>();
        _playerModel.PlayerLayer = LayerMask.NameToLayer("Player");
        _playerModel.GroundLayer = LayerMask.NameToLayer("Ground");
        _playerModel.TextTimeInSlowMove.gameObject.SetActive(false);
        _playerModel.WarningField.gameObject.SetActive(false);
        _playerModel.WarningFieldMoney.gameObject.SetActive(false);
        PlayerModel.Money = 300;
        _playerModel.MoneyField.text = PlayerModel.Money.ToString();
        PlayerModel.CanBuild = true;
    }

    private void FixedUpdate()
    {
        if (PlayerModel.CanMove)
        {
            if (!_playerModel.SlowMove)
            {
                _playerView.Move(_playerModel.Rigidbody, _playerModel.Animator, _playerModel.HorizontalInput, _playerModel.Speed);
                
            } 
            else
            {
                _playerView.MoveSlow(_playerModel.Rigidbody, _playerModel.Animator, _playerModel.HorizontalInput, _playerModel.Speed);
                if (_playerModel.CurrentTimeInSlowMove <= 0)
                {
                    _playerModel.SlowMove = false;
                    _playerModel.TextTimeInSlowMove.gameObject.SetActive(false);
                    _playerModel.Animator.SetBool("CanMoveSlow", false);
                }
                else
                {

                    _playerModel.TextTimeInSlowMove.text = "Время замедления: " + Mathf.Round(_playerModel.CurrentTimeInSlowMove).ToString();
                    _playerModel.CurrentTimeInSlowMove -= Time.deltaTime;
                }
            }
            
            _playerView.Climb(_playerModel.Rigidbody, _playerModel.Animator, _playerModel.VerticalInput, _playerModel.Speed, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer, _playerModel.PlayerLayer, _playerModel.GroundLayer);

            if (_playerModel.Rigidbody.velocity.y <= 0)
            {
                _playerView.Jump(_playerModel.Rigidbody, _playerModel.JumpInput, _playerModel.JumpForce, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer);
            }

            if (_playerModel.HorizontalInput > 0 || _playerModel.HorizontalInput < 0)
            {
                
                _playerModel.Sprite.flipX = _playerModel.HorizontalInput < 0;
                
            }
        } 
        else
        {
            _playerView.FixedPosition(_playerModel.Rigidbody);
        }

        if (!_playerView.CheckGroundForBuild(_playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer))
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
        if (PlayerModel.CanBuild && PlayerModel.Money >= 40)
        {
            if (Tower == "FireTower")
            {
                PlayerModel.Money -= 40;
            } 
            else if (Tower == "SlowedTower")
            {
                PlayerModel.Money -= 50;
            }
            else if (Tower == "ElectroTower")
            {
                PlayerModel.Money -= 60;
            }
            _playerModel.MoneyField.text = PlayerModel.Money.ToString();
            Instantiate(Resources.Load<GameObject>(Tower), new Vector2(transform.position.x, transform.position.y - 1.32f), Quaternion.identity);
        } 
        else if (PlayerModel.Money < 40)
        {
            StartCoroutine(_playerView.ShowWarning(_playerModel.WarningFieldMoney));
        }
        else
        {
            StartCoroutine(_playerView.ShowWarning(_playerModel.WarningField));
        }
        GlobalEventManager.CloseTowerMenu();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy") {
            _playerModel.TextTimeInSlowMove.gameObject.SetActive(true);
            _playerModel.SlowMove = true;
            _playerModel.CurrentTimeInSlowMove = _playerModel.TimeInSlowMove;
            _playerModel.Animator.SetBool("CanMove", false);
            Physics2D.IgnoreCollision(_playerModel.PlayerCollider, collision.collider, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Money")
        {
            _playerModel.MoneyField.text = PlayerModel.Money.ToString();
        }
    }
}
