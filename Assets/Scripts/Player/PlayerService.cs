using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerService : GenericLazySingleton<PlayerService>
{
    [SerializeField] private PlayerScriptableObject obj;

    private void Start()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        Level level = LevelManagerService.Instance.GetLevelBySceneName(SceneManager.GetActiveScene().name);

        PlayerModel model = new PlayerModel(obj);
        GameObject playerGameObject = Instantiate(model.PlayerViewGameObject, level.spawnPoint, Quaternion.identity);
        PlayerView view = playerGameObject.AddComponent<PlayerView>();
        PlayerController Player = new PlayerController(model, view);

        // Linking the MVC components
        model.SetPlayerController(Player);
        view.SetPlayerController(Player);
        TileService.Instance.AddPlayerTileController(playerGameObject);
        playerGameObject.transform.SetParent(TileService.Instance.transform, true);
    }

    public bool isPlayerMovementPossible(PlayerController playerController, Vector2 direction)
    {
        bool isMovementPossible = TileService.Instance.IsMovementPossibleForTileInDirection(direction, playerController.GetPlayerView().transform.position);
        for (int i = 0; i < playerController.GetPlayerView().transform.childCount; i++)
        {
            Transform childTile = playerController.GetPlayerView().transform.GetChild(i);
            bool isChildMovementPossible = TileService.Instance.IsMovementPossibleForTileInDirection(direction, childTile.position);
            isMovementPossible = isMovementPossible && isChildMovementPossible;
        }
        return isMovementPossible;
    }

    public List<GameObject> GetGameObjectsInDirection(Vector2 direction, PlayerController playerController)
    {
        List<GameObject> directionGameObjects = new List<GameObject>();
        TileController nearestTileInDirection = TileService.Instance.FetchTileAtPosition(playerController.GetPlayerView().transform.position + (Vector3)direction);
        if (nearestTileInDirection != null && nearestTileInDirection.tileStatus != TileStatus.WALL) {
            directionGameObjects.Add(nearestTileInDirection.gameObject);
        }
        for (int i = 0; i < playerController.GetPlayerView().transform.childCount; i++)
        {
            Transform childTransform = playerController.GetPlayerView().transform.GetChild(i);
            TileController nearestChildTileInDirection = TileService.Instance.FetchTileAtPosition(childTransform.position + (Vector3)direction);
            if (nearestChildTileInDirection != null && nearestChildTileInDirection.tileStatus != TileStatus.WALL)
            {
                directionGameObjects.Add(nearestChildTileInDirection.gameObject);
            }
        }
        return directionGameObjects;
    }
}
