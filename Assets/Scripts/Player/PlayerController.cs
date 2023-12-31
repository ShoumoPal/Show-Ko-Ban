using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

/* Player controller for MVC */

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
                if(!PlayerService.Instance.CheckIsChildOrPlayerAlreadyPresent(directionGameObject, this))
                {
                    AudioService.Instance.PlayFX2(SoundType.Connect_Sound);
                    directionGameObject.transform.SetParent(PlayerView.transform, true);
                }
            }

            // MOVE IF POSSIBLE
            bool isConnectedMovementsPossible = PlayerService.Instance.IsPlayerMovementPossible(this, direction);
            if (isConnectedMovementsPossible)
            {
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

    // Move Player Co-routine
    private IEnumerator StartMovement(Vector3 targetPosition)
    {
        PlayerModel.IsMoving = true;
        AudioService.Instance.PlayFX(SoundType.Move_Sound);
        PlayerView.transform.DOMove(targetPosition, PlayerView.duration);
        yield return new WaitForSeconds(PlayerView.duration);
        PlayerModel.IsMoving = false;

        if (EventService.Instance.InvokeOnGoalReached(this.PlayerView.transform.childCount))
        {
            Debug.Log("Goal reached!");
            //For when goal is reached
            LevelManagerService.Instance.SetCurrentLevelComplete();
            EventService.Instance.InvokeOnShowLevelCompletePanel();
        }
    }
}
