using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController PlayerController { get; set; }
    private PlayerModel _playerModel;

    [SerializeField] private Transform _movePoint;
    [SerializeField] private LayerMask _whatCanStopPlayer;
    [SerializeField] private LayerMask _whatCanAttachToPlayer;

    // Variables to check attached boxes
    private int _leftBoxes;
    private int _rightBoxes;
    private int _topBoxes;
    private int _bottomBoxes;

    private Collider2D _leftWallHit;
    private Collider2D _rightWallHit;
    private Collider2D _bottomWallHit;
    private Collider2D _topWallHit;

    private Collider2D _leftBoxHit;
    private Collider2D _rightBoxHit;
    private Collider2D _bottomBoxHit;
    private Collider2D _topBoxHit;

    public void SetPlayerController(PlayerController _playerController)
    {
        PlayerController = _playerController;
    }

    private void Awake()
    {
        _movePoint.parent = null;
        _leftBoxes = 0;
        _rightBoxes = 0;
        _topBoxes = 0;
        _bottomBoxes = 0;
    }
    private void Start()
    {
        _playerModel = PlayerController.GetPlayerModel();
    }
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float _horizontal = Input.GetAxisRaw("Horizontal");
        float _vertical = Input.GetAxisRaw("Vertical");

        // Move object to Move point position
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _playerModel.Speed * Time.deltaTime);

        // Check input only when player has reached move point
        if (Vector3.Distance(transform.position, _movePoint.position) < 0.05f)
        {
            if(_horizontal == 1f) // Right motion
            {
                _rightWallHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(_horizontal + _rightBoxes, 0f, 0f), 0.2f, _whatCanStopPlayer);
                _rightBoxHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(_horizontal + _rightBoxes, 0f, 0f), 0.2f, _whatCanAttachToPlayer);
                Debug.Log("Right hits: " + _rightBoxHit, _rightWallHit);
                if (_rightBoxHit)
                {
                    ++_rightBoxes;
                    _rightBoxHit.transform.SetParent(transform);
                }
                else if (!_rightWallHit)
                {
                    _movePoint.position += new Vector3(1f, 0f, 0f);
                }
            }  
            else if(_horizontal == -1f) // Left motion
            {
                _leftWallHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(_horizontal - _leftBoxes, 0f, 0f), 0.2f, _whatCanStopPlayer);
                _leftBoxHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(_horizontal - _leftBoxes, 0f, 0f), 0.2f, _whatCanAttachToPlayer);
                Debug.Log("Left hits: " + _leftBoxHit, _leftWallHit);
                if (_leftBoxHit)
                {
                    ++_leftBoxes;
                    _leftBoxHit.transform.SetParent(transform);
                }
                else if (!_leftWallHit)
                {
                    _movePoint.position += new Vector3(-1f, 0f, 0f);
                }
            }
            else if(_vertical == 1f) // Top motion
            {
                _topWallHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, _vertical + _topBoxes, 0f), 0.2f, _whatCanStopPlayer);
                _topBoxHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, _vertical + _topBoxes, 0f), 0.2f, _whatCanAttachToPlayer);
                if (_topBoxHit)
                {
                    ++_topBoxes;
                    _topBoxHit.transform.SetParent(transform);
                }
                else if (!_topWallHit)
                {
                    _movePoint.position += new Vector3(0f, 1f, 0f);
                }
            }
            else if(_vertical == -1f) // Bottom motion
            {
                _bottomWallHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, _vertical - _bottomBoxes, 0f), 0.2f, _whatCanStopPlayer);
                _bottomBoxHit = Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, _vertical - _bottomBoxes, 0f), 0.2f, _whatCanAttachToPlayer);
                if(_bottomBoxHit)
                {
                    ++_bottomBoxes;
                    _bottomBoxHit.transform.SetParent(transform);
                }
                else if (!_bottomWallHit)
                {
                    _movePoint.position += new Vector3(0f, -1f, 0f);
                }
            }
        }
    }
}
