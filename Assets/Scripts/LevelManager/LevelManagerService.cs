using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* public class for keeping level information */

[Serializable]
public class Level
{
    public string LevelName;
    public Vector3 spawnPoint;
}

/* Enum for storing status of the levels */

public enum LevelStatus
{
    Locked,
    Unlocked,
    Completed
}

/* Level manager service which persists throughout the game */

public class LevelManagerService : GenericMonoSingleton<LevelManagerService>
{
    public Level[] Levels;
    private bool hasRestarted;

    private void OnEnable()
    {
        EventService.Instance.OnRestartClicked += RestartClicked;
    }

    private void Start()
    {
        AudioService.Instance.PlayBG(SoundType.BG_Music_1);
        hasRestarted = false;
        SetLevelStatus(Levels[0].LevelName, LevelStatus.Unlocked);
    }

    private void Update()
    {
        // Restart level logic
        if(Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex != 0)
            RestartCurrentLevel();
    }

    // Co-routine for loading the scenes with transition */

    public IEnumerator LoadScene(string _levelName)
    {
        if(_levelName == Levels[0].LevelName)
        {
            AudioService.Instance.StopBG2();
        }
        if(_levelName == Levels[4].LevelName)
        {
            AudioService.Instance.PlayBG2(SoundType.Interstellar);
        }

        CrossfadeService.Instance.CrossFadeIn(_levelName);
        yield return new WaitForSeconds(CrossfadeService.Instance.CrossFadeTime);
        SceneManager.LoadScene(_levelName);
        CrossfadeService.Instance.CrossFadeOut();
        yield return new WaitForSeconds(CrossfadeService.Instance.CrossFadeTime);
    }

    private bool RestartClicked()
    {
        return hasRestarted;
    }

    public void SetRestartClicked(bool _clicked)
    {
        hasRestarted = _clicked;
    }

    public Level GetLevelBySceneName(string sceneName)
    {
        Level level = Array.Find(Levels, i => i.LevelName == sceneName);
        return level;
    }

    public LevelStatus GetLevelStatus(string levelName)
    {
        return (LevelStatus)PlayerPrefs.GetInt(levelName);
    }

    public void SetLevelStatus(string  levelName, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(levelName, (int)levelStatus);
    }

    private string GetLevelNameFromIndex(int index)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(index);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf(".");
        return name.Substring(0, dot);
    }

    public void SetCurrentLevelComplete()
    {
        SetLevelStatus(SceneManager.GetActiveScene().name, LevelStatus.Completed);

        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % (Levels.Length + 1);
        string nextLevelName = GetLevelNameFromIndex(nextSceneIndex);

        SetLevelStatus(nextLevelName, LevelStatus.Unlocked);
    }

    public void LoadNextLevel()
    {
        hasRestarted = false;
        int sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;

        StartCoroutine(LoadScene(GetLevelNameFromIndex(sceneNumber % (Levels.Length + 1))));
    }

    public void RestartCurrentLevel()
    {
        hasRestarted = true;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        EventService.Instance.OnRestartClicked -= RestartClicked;
    }
}
