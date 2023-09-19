using System;
using UnityEngine;

public class LevelManagerService : GenericLazySingleton<LevelManagerService>
{
    public Level[] Levels;

    public Level GetLevelBySceneName(string sceneName)
    {
        Level level = Array.Find(Levels, i => i.LevelName == sceneName);
        return level;
    }
}
[Serializable]
public class Level
{
    public string LevelName;
    public Vector3 spawnPoint;
}