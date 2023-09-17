using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController PlayerController { get; set; }

    private bool _isMoving;
    private float _moveDuration;

    public void SetPlayerController(PlayerController _playerController)
    {
        PlayerController = _playerController;
    }

    private void Awake()
    {
        _moveDuration = 0.2f;
        _isMoving = false;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!_isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                StartCoroutine(Move(Vector2.up));
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                StartCoroutine(Move(Vector2.left));
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                StartCoroutine(Move(Vector2.down));
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                StartCoroutine(Move(Vector2.right));
        }
    }
    private IEnumerator Move(Vector2 direction)
    {
        // Record movement to not accept more input
        _isMoving = true;

        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + direction;

        float elapsedTime = 0;
        while (elapsedTime < _moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / _moveDuration;
            transform.position = Vector2.Lerp(startPos, endPos, percent);
            yield return null;
        }

        // No longer moving, hence can accept input
        _isMoving = false;
    }
}
