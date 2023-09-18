using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController PlayerController { get; set; }
    public float duration = 0.2f;

    
    public void SetPlayerController(PlayerController _playerController)
    {
        PlayerController = _playerController;
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.RightArrow) && !PlayerController.GetPlayerModel().IsMoving)
        {
            direction = Vector2.right;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && !PlayerController.GetPlayerModel().IsMoving) 
        { 
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !PlayerController.GetPlayerModel().IsMoving)
        { 
            direction = Vector2.up; 
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !PlayerController.GetPlayerModel().IsMoving) 
        { 
            direction = Vector2.down; 
        }

        if (direction != Vector2.zero)
        {
            Debug.Log(direction);
            PlayerController.MoveConnectedComponents(direction);
        }
        
    }
}
