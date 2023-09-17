using System.Collections;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public PlayerController PlayerController { get; set; }
    private PlayerModel _playerModel;

    [SerializeField] private Transform _movePoint;
    [SerializeField] private LayerMask _whatCanStopPlayer;

    public void SetPlayerController(PlayerController _playerController)
    {
        PlayerController = _playerController;
    }

    private void Awake()
    {
        _movePoint.parent = null;
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
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _playerModel.Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _movePoint.position) < 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(_movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, _whatCanStopPlayer))
                    _movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
                
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, _whatCanStopPlayer))
                    _movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }    
        }
    }
}
