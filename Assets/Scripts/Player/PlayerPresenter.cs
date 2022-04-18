using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerView _playerView;

    private void Start()
    {
        _playerView = GetComponent<PlayerView>();
        PlayerModel.CanBuild = true;
    }

    private void FixedUpdate()
    {
        if (PlayerModel.CanMove)
        {
            _playerView.Move();
            _playerView.Climb();
            _playerView.Jump();
        } else
        {
            _playerView.FixedPosition();
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
