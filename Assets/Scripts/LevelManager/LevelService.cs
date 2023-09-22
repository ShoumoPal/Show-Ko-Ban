using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        AudioService.Instance.PlayFX2(SoundType.PopUp_Sound);
        StartCoroutine(HidePanelWithAnimation(PanelType.PAUSE));
    }

    private void GoToLobby()
    {
        LevelManagerService.Instance.SetRestartClicked(false);
        Time.timeScale = 1f;
        StartCoroutine(LevelManagerService.Instance.LoadScene("Lobby"));
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        AudioService.Instance.PlayFX(SoundType.PopUp_Sound);
        LevelManagerService.Instance.RestartCurrentLevel();
    }

    private void ShowPauseMenu()
    {
        AudioService.Instance.PlayFX2(SoundType.PopUp_Sound);
        StartCoroutine(ShowPanelWithAnimation(PanelType.PAUSE));
    }

    private void GoToNextLevel()
    {
        Time.timeScale = 1f;
        LevelManagerService.Instance.LoadNextLevel();
    }

    public void ShowLevelCompletePanel()
    {
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
