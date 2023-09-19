using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileService : GenericLazySingleton<TileService>
{

    List<TileController> tileControllers;

    private void Start()
    {
        tileControllers = new List<TileController>();
        GameObject[] tileGameObjects = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0;  i < tileGameObjects.Length; i++)
        {
            tileControllers.Add(tileGameObjects[i].GetComponent<TileController>());
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
