using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "SO's/Player")]
public class PlayerScriptableObject : ScriptableObject
{
    public GameObject PlayerViewPrefab;
    public float speed;
}
