using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* Message Box controller which manages the dialogue boxes for tutorials and tips */

public class MessageBoxController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private RectTransform _messageBoxRT;
    [SerializeField] private CanvasGroup _messageCG;
    [SerializeField] private MessageScriptableObject _messageObj;
    [SerializeField] private float _fadeTime;

    private Queue<string> _messagesQueue;

    private void Start()
    {
        _messagesQueue = new Queue<string>();
        Debug.Log(EventService.Instance.HasRestarted());
        if(!EventService.Instance.HasRestarted())
            StartCoroutine(ShowMessages());
    }

    private IEnumerator ShowMessages()
    {
        AudioService.Instance.PlayFX2(SoundType.PopUp_Sound);
        MessageFadeIn();

        foreach(string message in _messageObj.Messages)
        {
            _messagesQueue.Enqueue(message);
        }

        while(_messagesQueue.Count > 0)
        {
            AudioService.Instance.PlayFX2(SoundType.PopUp_Sound);
            DisplayNextMessage();
            yield return new WaitForSeconds(3f);
        }

        AudioService.Instance.PlayFX2(SoundType.PopUp_Sound);
        MessageFadeOut();
    }

    private void DisplayNextMessage()
    {
        if(_messagesQueue.Count == 0)
        {
            return;
        }

        string message = _messagesQueue.Dequeue();
        StartCoroutine(TypeMessage(message));
    }

    private IEnumerator TypeMessage(string _message)
    {
        _messageText.text = "";
        foreach(char letter in _message.ToCharArray())
        {
            _messageText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void MessageFadeIn()
    {
        _messageCG.gameObject.SetActive(true);
        _messageCG.alpha = 0f;
        _messageBoxRT.transform.localPosition = new Vector3(1030f, -1000f, 0f);
        _messageBoxRT.DOAnchorPos(new Vector2(1030f, 140f), _fadeTime, false).SetEase(Ease.InOutBounce);
        _messageCG.DOFade(1f, _fadeTime);
    }

    private void MessageFadeOut()
    {
        _messageCG.alpha = 1f;
        _messageBoxRT.transform.localPosition = new Vector3(1030f, 140f, 0f);
        _messageBoxRT.DOAnchorPos(new Vector2(1030f, -1000f), _fadeTime, false).SetEase(Ease.InElastic);
        _messageCG.DOFade(0f, _fadeTime);
        _messageCG.gameObject.SetActive(false);
    }
}
