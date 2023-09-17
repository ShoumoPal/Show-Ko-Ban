using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerService : GenericMonoSingleton<PlayerService>
{
    public PlayerController Player;
    [SerializeField] private PlayerScriptableObject obj;

    private void Start()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        Level level = LevelManagerService.Instance.GetLevelBySceneName(SceneManager.GetActiveScene().name);

        PlayerModel model = new PlayerModel(obj);
        PlayerView view = Instantiate<PlayerView>(model.PlayerView, level.spawnPoint, Quaternion.identity);
        Player = new PlayerController(model, view);

        // Linking the MVC components
        model.SetPlayerController(Player);
        view.SetPlayerController(Player);
    }
}
