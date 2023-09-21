using System;
using UnityEngine;

public class EventService : GenericLazySingleton<EventService>
{
    public event Func<int, bool> OnGoalReached;
    public event Action OnShowLevelCompletePanel;
    public event Action OnCameraShake;

    public bool InvokeOnGoalReached(int childCount)
    {
        return (bool)OnGoalReached?.Invoke(childCount);
    }
    public void InvokeOnShowLevelCompletePanel()
    {
        OnShowLevelCompletePanel?.Invoke();
    }
    public void InvokeOnCameraShake()
    {
        OnCameraShake?.Invoke();
    }
}
