using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TileService : GenericLazySingleton<TileService>
{
    List<TileController> tileControllers;
    [SerializeField] private Transform _wallParent;
    [SerializeField] private Transform _blockParent;
    [SerializeField] private Transform _hiddenParent;

    private void Start()
    {
        tileControllers = new List<TileController>();
        foreach(Transform child in _wallParent)
        {
            tileControllers.Add(child.gameObject.GetComponent<TileController>());
        }
        foreach (Transform child in _blockParent)
        {
            tileControllers.Add(child.gameObject.GetComponent<TileController>());
        }
        foreach(Transform child in _hiddenParent)
        {
            child.GetComponent<TileController>().tileStatus = TileStatus.HIDDEN;
            tileControllers.Add(child.gameObject.GetComponent<TileController>());
        }
    }

    public void AddPlayerTileController(GameObject playerGameObject)
    {
        tileControllers.Add(playerGameObject.GetComponent<TileController>());
    }

    public TileController FetchTileAtPosition(Vector3 position)
    {
        foreach (TileController controller in tileControllers)
        {
            Vector3 tilePosition = controller.transform.position;
            if (tilePosition == position)
            {
                return controller;
            }
        }
        return null;
    }

    public bool IsMovementPossibleForTileInDirection(Vector2 direction, Vector3 tileControllerPosition)
    {
        Vector3 targetPosition = (Vector3)direction + tileControllerPosition;
        TileController targetTile = FetchTileAtPosition(targetPosition);

        if (targetTile == null)
        {
            return true;
        } 
        else if (targetTile.tileStatus == TileStatus.WALL)
        {
            return false;
        } 
        else
        {
            return true;
        }
    }
}
