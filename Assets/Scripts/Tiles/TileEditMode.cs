using UnityEngine;

// EditMode script 

[ExecuteAlways]
public class TileEditMode : MonoBehaviour
{
    [SerializeField] Sprite WallTileSprite;
    [SerializeField] Sprite BlockTileSprite;
    [SerializeField] Sprite PlayerTileSprite;
    [SerializeField] Sprite HiddenTileSprite;
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
        } 
        else if (tileStatus == TileStatus.PLAYER)
        {
            tileController.tileStatus = TileStatus.PLAYER;
            spriteRenderer.sprite = PlayerTileSprite;
        }
        else if(tileStatus == TileStatus.HIDDEN)
        {
            tileController.tileStatus = TileStatus.HIDDEN;
            spriteRenderer.sprite = HiddenTileSprite;
        }
        else
        {
            tileController.tileStatus = TileStatus.WALL;
            spriteRenderer.sprite= WallTileSprite;
        }

        // Snapping block to grid
        Vector3 currentTilePosition = transform.position;
        transform.position = new Vector3(Mathf.RoundToInt(currentTilePosition.x), Mathf.RoundToInt(currentTilePosition.y), 0f);
    }
}
