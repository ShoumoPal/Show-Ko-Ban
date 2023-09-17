using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "SO's/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    public PlayerView PlayerView;
    public float speed;
}
