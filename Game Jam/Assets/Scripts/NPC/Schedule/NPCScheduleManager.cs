using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class NPCScheduleManager : MonoBehaviour
{
    private static NPCScheduleManager _instance;

    public static NPCScheduleManager Instance
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
    
    [SerializedDictionary("Hour (from 0 to 23)", "Waypoint")]
    [SerializeField] private SerializedDictionary<int, Transform> _scheduledWaypoints;
    
    public SerializedDictionary<int, Transform> ScheduledWaypoints
    {
        get => _scheduledWaypoints;
        private set => _scheduledWaypoints = value;
    }

    private void Awake() => Instance = this;
}
