using System;
using UnityEngine;

public class EventService : GenericLazySingleton<EventService>
{
    public event Func<int, bool> OnGoalReached;

    public bool InvokeOnGoalReached(int childCount)
    {
        return (bool)OnGoalReached?.Invoke(childCount);
    }
}
