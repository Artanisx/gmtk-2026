using UnityEngine.Events;

/// <summary>
/// Static class that has every event and event invoke method, does not need to be attached to GameObject
/// </summary>
[System.Serializable]
public static class EventManager
{
    public static UnityEvent<int, int> TimeHasChanged = new UnityEvent<int, int>();
    public static UnityEvent PlayerLosed = new UnityEvent();
    public static UnityEvent PlayerWin = new UnityEvent();
    
    public static void NotifyTimeChanged(int hour, int minute)
    {
        TimeHasChanged?.Invoke(hour, minute);
    }

    public static void NotifyPlayerLosed()
    {
        PlayerLosed?.Invoke();
    }

    public static void NotifyPlayerWin()
    {
        PlayerWin?.Invoke();
    }
}
