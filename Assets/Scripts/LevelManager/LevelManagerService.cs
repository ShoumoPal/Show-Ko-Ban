using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Level
{
    public string LevelName;
    public Vector3 spawnPoint;
}

public enum LevelStatus
{
    Locked,
    Unlocked,
    Completed
}

public class LevelManagerService : GenericMonoSingleton<LevelManagerService>
{
    public Level[] Levels;

    private void Start()
    {
        AudioService.Instance.PlayBG(SoundType.BG_Music_1);
        SetLevelStatus(Levels[0].LevelName, LevelStatus.Unlocked);
    }

    private void Update()
    {
        // Restart level logic
        if(Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex != 0)
            RestartCurrentLevel();
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

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string nextLevelName = GetLevelNameFromIndex(nextSceneIndex);

        SetLevelStatus(nextLevelName, LevelStatus.Unlocked);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
