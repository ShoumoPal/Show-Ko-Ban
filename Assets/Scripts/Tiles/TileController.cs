using UnityEngine;

/* Tile controller for each tile */

public class TileController : MonoBehaviour
{
    public TileStatus tileStatus;
    public TileEditMode tileEditMode;

    private void Awake()
    {
        tileEditMode = GetComponent<TileEditMode>();
        tileEditMode.enabled = false;
    }
}
