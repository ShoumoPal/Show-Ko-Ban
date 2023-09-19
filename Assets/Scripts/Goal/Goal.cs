﻿using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool isOccupied;

    private void Start()
    {
        isOccupied = false;
    }

    public bool GetOccupied()
    {
        return isOccupied;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    TileController tileController = collision.GetComponent<TileController>();
    //    if(tileController)
    //        if(tileController.tileStatus == TileStatus.PLAYER || tileController.tileStatus == TileStatus.BLOCK)
    //            isOccupied = true;
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        TileController tileController = collision.GetComponent<TileController>();
        if (tileController)
            if (tileController.tileStatus == TileStatus.PLAYER || tileController.tileStatus == TileStatus.BLOCK)
                isOccupied = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TileController tileController = collision.GetComponent<TileController>();
        if (tileController)
            if (tileController.tileStatus == TileStatus.PLAYER || tileController.tileStatus == TileStatus.BLOCK)
                isOccupied = true;
    }
}