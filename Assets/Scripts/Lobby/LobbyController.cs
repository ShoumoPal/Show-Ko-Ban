using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button _levelSelectionButton;
    [SerializeField] private Button _levelSelectionBackButton;
    [SerializeField] private CanvasGroup _backgroundUI;

    private void Awake()
    {
        _levelSelectionButton.onClick.AddListener(ShowLevelSelectionPanel);
        _levelSelectionBackButton.onClick.AddListener(HideLevelSelectionPanel);
    }

    private void ShowLevelSelectionPanel()
    {
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        _backgroundUI.blocksRaycasts = false;
        UIManager.Instance.PanelFadeIn();
    }

    private void HideLevelSelectionPanel()
    {
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        _backgroundUI.blocksRaycasts = true;
        StartCoroutine(UIManager.Instance.PanelFadeOut());
    }
}
