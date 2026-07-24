using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;

    public static TimeManager Instance
    {
        get => _instance;
        private set
        {
            if (_instance == null)
            {
                _instance = value;
            }

            if (_instance != value)
            {
                Destroy(value);
            }
        }
    }
    
    [SerializeField] private float _minutesThreshold;
    private float _currentSeconds;
    
    // minutesThreshold now descript the duration of an hour in second
    private int _currentHour = 8;

    public int CurrentHour
    {
        get => _currentHour;
        private set
        {
            _currentHour = value;
         
            if (value >= 24)
            {
                _currentHour = 0;
            }
            
            EventManager.NotifyTimeChanged(_currentHour, CurrentMinute);
        }
    }

    public int CurrentMinute
    {
        get {return (int)(_currentSeconds/_minutesThreshold * 60);}
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update() => UpdateTime();

    private void UpdateTime()
    {
        _currentSeconds += Time.deltaTime;

        if (_currentSeconds >= _minutesThreshold)
        {
            _currentSeconds = 0;
            CurrentHour++;
        }

        EventManager.NotifyTimeChanged(_currentHour, CurrentMinute);
    }
}
