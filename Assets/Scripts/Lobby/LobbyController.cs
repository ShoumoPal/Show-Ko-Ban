using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button _levelSelectionButton;
    [SerializeField] private Button _levelSelectionBackButton;
    [SerializeField] private CanvasGroup _backgroundUI;
    [SerializeField] private Slider _playSlider;

    private void Awake()
    {
        _levelSelectionButton.onClick.AddListener(ShowLevelSelectionPanel);
        _levelSelectionBackButton.onClick.AddListener(HideLevelSelectionPanel);
        _playSlider.onValueChanged.AddListener(CheckSliderFull);
    }

    private void CheckSliderFull(float value)
    {
        if (_playSlider.value == _playSlider.maxValue)
        {
            StartCoroutine(LevelManagerService.Instance.LoadScene(LevelManagerService.Instance.Levels[0].LevelName));
            AudioService.Instance.PlayFX(SoundType.Win_Sound);
        } 
    }

    private void ShowLevelSelectionPanel()
    {
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        _backgroundUI.blocksRaycasts = false;
        UIManager.Instance.PanelFadeIn(PanelType.LEVEL_SELECTION);
    }

    private void HideLevelSelectionPanel()
    {
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        _backgroundUI.blocksRaycasts = true;
        StartCoroutine(UIManager.Instance.PanelFadeOut(PanelType.LEVEL_SELECTION));
    }
}
