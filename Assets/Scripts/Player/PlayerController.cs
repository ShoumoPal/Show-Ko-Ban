using UnityEngine;

public class PlayerController
{
    public PlayerModel PlayerModel { get; set; }
    public PlayerView PlayerView { get; set; }

    public PlayerController(PlayerModel playerModel, PlayerView playerView)
    {
        PlayerModel = playerModel;
        PlayerView = playerView;
    }
}
