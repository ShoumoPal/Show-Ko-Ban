using UnityEngine;

/* Player model for MVC */

public class PlayerModel
{
    public PlayerController PlayerController { get; set; }
    public GameObject PlayerViewGameObject { get; set; }
    public float Speed{ get; set; }
    public bool IsMoving { get; set; }

    public PlayerModel(PlayerScriptableObject obj)
    {
        PlayerViewGameObject = obj.PlayerViewPrefab;
        Speed = obj.speed;
        IsMoving = false;
    }

    public void SetPlayerController(PlayerController _playerController)
    {
        PlayerController = _playerController;
    }
}
