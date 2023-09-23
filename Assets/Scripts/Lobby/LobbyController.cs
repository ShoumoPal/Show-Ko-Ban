using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/* Lobby controller which keeps track and manages all the UI elements in the lobby screen */

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button _levelSelectionButton;
    [SerializeField] private Button _levelSelectionBackButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private CanvasGroup _backgroundUI;
    [SerializeField] private Slider _playSlider;
    [SerializeField] private RectTransform _startTextRT;
    [SerializeField] private Transform _block1;
    [SerializeField] private Transform _block2;

    private void Awake()
    {
        _levelSelectionButton.onClick.AddListener(ShowLevelSelectionPanel);
        _levelSelectionBackButton.onClick.AddListener(HideLevelSelectionPanel);
        _playSlider.onValueChanged.AddListener(CheckSliderFull);
        _quitButton.onClick.AddListener(QuitGame);
        MoveText();
        RotateBlock(_block1);
        RotateBlock(_block2);
    }

    private void RotateBlock(Transform block)
    {
        block.transform.DORotate(new Vector3(0f, 0f, 360f), 2f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        block.transform.DOScale(0.5f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void MoveText()
    {
        _startTextRT.DOAnchorPos(new Vector3(70f, 100f, 0f), 2f, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void QuitGame()
    {
        Application.Quit();
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
