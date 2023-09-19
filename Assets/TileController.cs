using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
