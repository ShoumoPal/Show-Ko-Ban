using UnityEngine;

/* Scriptable object for different messages in the dialogue boxes */

[CreateAssetMenu(fileName = "MessageScriptableObject", menuName = "SO's/NewMessageScriptableObject")]
public class MessageScriptableObject : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] Messages;
}
