using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class TileEditMode : MonoBehaviour
{
    [SerializeField] Sprite WallTileSprite;
    [SerializeField] Sprite BlockTileSprite;
    [SerializeField] Sprite PlayerTileSprite;
    [SerializeField] TileStatus tileStatus;

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        TileController tileController = GetComponent<TileController>();
        if (tileStatus == TileStatus.BLOCK)
        {
            tileController.tileStatus = TileStatus.BLOCK;
            spriteRenderer.sprite = BlockTileSprite;
        } else if (tileStatus == TileStatus.PLAYER)
        {
            tileController.tileStatus = TileStatus.PLAYER;
            spriteRenderer.sprite = PlayerTileSprite;
        } else
        {
            tileController.tileStatus = TileStatus.WALL;
            spriteRenderer.sprite= WallTileSprite;
        }
    }
}
