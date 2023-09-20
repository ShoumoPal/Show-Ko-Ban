using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteService : GenericMonoSingleton<LevelCompleteService>
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private GameObject _levelCompletePanel;

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(GoToNextLevel);
    }
    private void OnEnable()
    {
        EventService.Instance.OnShowLevelCompletePanel += ShowLevelCompletePanel;
    }
    private void GoToNextLevel()
    {
        Time.timeScale = 1f;
        LevelManagerService.Instance.LoadNextLevel();
    }

    public void ShowLevelCompletePanel()
    {
        AudioService.Instance.PlayFX(SoundType.Win_Sound);
        StartCoroutine(ShowPanelWithAnimation());
    }

    private IEnumerator ShowPanelWithAnimation()
    {
        UIManager.Instance.PanelFadeIn();
        yield return new WaitForSeconds(UIManager.Instance.GetFadeTime());
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        EventService.Instance.OnShowLevelCompletePanel -= ShowLevelCompletePanel;
    }
}
