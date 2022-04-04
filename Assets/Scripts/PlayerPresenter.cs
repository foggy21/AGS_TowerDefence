using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;
    private PlayerView _playerView;

    private void Start()
    {
        _playerView = gameObject.AddComponent<PlayerView>();
        _playerModel.Ground = LayerMask.GetMask("Ground");
        _playerModel.Ladder = LayerMask.GetMask("Ladder");
    }

    private void FixedUpdate()
    {
        if (_playerModel.HorizontalInput > 0 || _playerModel.HorizontalInput < 0) _playerView.Move(_playerModel.Rigidbody, _playerModel.HorizontalInput, _playerModel.Speed);
        if (_playerModel.VerticalInput > 0 || _playerModel.VerticalInput < 0) _playerView.Climb(_playerModel.Rigidbody, _playerModel.VerticalInput, _playerModel.Speed, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer, _playerModel.Ladder);
        if (_playerModel.JumpInput > 0) _playerView.Jump(_playerModel.Rigidbody, _playerModel.JumpInput, _playerModel.JumpForce, _playerModel.LayerChecker, _playerModel.CircleRadiusCheckingLayer, _playerModel.Ground);
    }
}
