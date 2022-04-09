using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerView _playerView;

    private void Start()
    {
        _playerView = GetComponent<PlayerView>();
    }

    private void FixedUpdate()
    {
        if (PlayerModel.CanMove)
        {
            _playerView.Move();
            _playerView.Climb();
            _playerView.Jump();
        }
        
    }
}
