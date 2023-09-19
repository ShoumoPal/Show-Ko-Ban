using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Rendering;

public class PlayerController 
{
    public PlayerModel PlayerModel { get; set; }
    public PlayerView PlayerView { get; set; }

    public PlayerController(PlayerModel playerModel, PlayerView playerView)
    {
        PlayerModel = playerModel;
        PlayerView = playerView;
    }

    public PlayerModel GetPlayerModel()
    {
        return PlayerModel;
    }

    public PlayerView GetPlayerView()
    {
        return PlayerView;
    }

    public void MoveConnectedComponents(Vector2 direction)
    {
        if (direction == Vector2.left || direction == Vector2.right || direction == Vector2.up || direction == Vector2.down)
        {
            // ADD CHILDREN IF IN THE SAME DIRECTION
            List<GameObject> directionGameObjects = PlayerService.Instance.GetGameObjectsInDirection(direction, this);
            foreach (GameObject directionGameObject in directionGameObjects)
            {
                directionGameObject.transform.SetParent(PlayerView.transform, true);
            }

            // MOVE IF POSSIBLE
            bool isConnectedMovementsPossible = PlayerService.Instance.isPlayerMovementPossible(this, direction);
            if (isConnectedMovementsPossible)
            {
                Debug.Log("Starting Coroutine.");
                Vector3 targetPosition = PlayerView.transform.position + (Vector3)direction;
                PlayerView.StartCoroutine(StartMovement(targetPosition));
            }
        }
        else
        {
            Debug.Log("Press One Arrow Key at a time.");
            return;
        }
    }

    private IEnumerator StartMovement(Vector3 targetPosition)
    {
        PlayerModel.IsMoving = true;
        PlayerView.transform.DOMove(targetPosition, PlayerView.duration);
        yield return new WaitForSeconds(PlayerView.duration);
        PlayerModel.IsMoving = false;

        if (EventService.Instance.InvokeOnGoalReached(this.PlayerView.transform.childCount))
            Debug.Log("Goal reached!");
    }
}
