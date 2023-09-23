using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/* Level service for each level containing the UI */

public class LevelService : GenericMonoSingleton<LevelService>
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _homeButton2;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _gamePauseMenu;

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(GoToNextLevel);
        _pauseButton.onClick.AddListener(ShowPauseMenu);
        _homeButton.onClick.AddListener(GoToLobby);
        _homeButton2.onClick.AddListener(GoToLobby);
        _restartButton.onClick.AddListener(RestartLevel);
        _playButton.onClick.AddListener(ResumeLevel);
    }

    private void OnEnable()
    {
        EventService.Instance.OnShowLevelCompletePanel += ShowLevelCompletePanel;
    }

    private void ResumeLevel()
    {
        _pauseButton.interactable = true;
        AudioService.Instance.PlayFX2(SoundType.PopUp_Sound);
        StartCoroutine(HidePanelWithAnimation(PanelType.PAUSE));
    }

    private void GoToLobby()
    {
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        AudioService.Instance.StopBG2();
        LevelManagerService.Instance.SetRestartClicked(false);
        Time.timeScale = 1f;
        _pauseButton.interactable = true;
        StartCoroutine(LevelManagerService.Instance.LoadScene("Lobby"));
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        _pauseButton.interactable = true;
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        LevelManagerService.Instance.RestartCurrentLevel();
    }

    private void ShowPauseMenu()
    {
        AudioService.Instance.PlayFX2(SoundType.PopUp_Sound);
        _pauseButton.interactable = false;
        StartCoroutine(ShowPanelWithAnimation(PanelType.PAUSE));
    }

    private void GoToNextLevel()
    {
        Time.timeScale = 1f;
        _pauseButton.interactable = true;
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        LevelManagerService.Instance.LoadNextLevel();
    }

    public void ShowLevelCompletePanel()
    {
        _pauseButton.interactable = false;
        AudioService.Instance.PlayFX(SoundType.Win_Sound);
        StartCoroutine(ShowPanelWithAnimation(PanelType.LEVEL_COMPLETE));
    }

    private IEnumerator ShowPanelWithAnimation(PanelType type)
    {
        UIManager.Instance.PanelFadeIn(type);
        yield return new WaitForSeconds(UIManager.Instance.GetFadeTime());
        Time.timeScale = 0f;
    }

    private IEnumerator HidePanelWithAnimation(PanelType type)
    {
        Time.timeScale = 1f;
        StartCoroutine(UIManager.Instance.PanelFadeOut(type));
        yield return new WaitForSeconds(UIManager.Instance.GetFadeTime());
    }

    private void OnDisable()
    {
        EventService.Instance.OnShowLevelCompletePanel -= ShowLevelCompletePanel;
    }
}
