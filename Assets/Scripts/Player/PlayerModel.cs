using UnityEngine;

public class PlayerModel
{
    public PlayerController PlayerController { get; set; }
    public PlayerView PlayerView { get; set; }
    public float Speed{ get; set; }

    public PlayerModel(PlayerScriptableObject obj)
    {
        PlayerView = obj.PlayerView;
        Speed = obj.speed;
    }

    public void SetPlayerController(PlayerController _playerController)
    {
        PlayerController = _playerController;
    }
}
