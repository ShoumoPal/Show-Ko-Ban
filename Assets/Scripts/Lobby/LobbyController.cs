using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button _levelSelectionButton;
    [SerializeField] private GameObject _levelSelectionPanel;

    private void Awake()
    {
        _levelSelectionButton.onClick.AddListener(ShowLevelSelectionPanel);
    }

    private void ShowLevelSelectionPanel()
    {
        _levelSelectionPanel.SetActive(true);
        UIManager.Instance.PanelFadeIn();
    }
}
