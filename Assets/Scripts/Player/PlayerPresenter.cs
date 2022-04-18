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
        PlayerModel.CanBuild = true;
    }

    private void FixedUpdate()
    {
        if (!_playerView.CheckGround(_playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer))
        {
            PlayerModel.CanBuild = false;
        }
        else
        {
            PlayerModel.CanBuild = true;
        }


        if (PlayerModel.CanMove)
        {
            _playerView.Move(_playerModel.Rigidbody, _playerModel.HorizontalInput, _playerModel.Speed);
            _playerView.Climb(_playerModel.Rigidbody, _playerModel.VerticalInput, _playerModel.Speed, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer);
            _playerView.Jump(_playerModel.Rigidbody, _playerModel.JumpInput, _playerModel.JumpForce, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer);
        } else
        {
            _playerView.FixedPosition(_playerModel.Rigidbody);
        }
        
    }

    public void BuildTower(string Tower)
    {
        if (PlayerModel.CanBuild)
        {
            Instantiate(Resources.Load<GameObject>(Tower), new Vector2(transform.position.x, transform.position.y - 1f), Quaternion.identity);
        }
        GlobalEventManager.CloseTowerMenu();
    }
}
