using System;
using TMPro;
using UnityEngine;

//TODO: Made one master class that has one schedule that every npc guard is following
public class ScheduleUI : MonoBehaviour
{
    [SerializeField] private NPCMovementPatrol _patrol;
    [SerializeField] private TextMeshProUGUI _scheduleText;
    private string _text = "";
    
    private void Awake()
    {
        foreach (var schedulePoint in _patrol.ScheduledWaypoints)
        {
            var text = $"At {schedulePoint.Key}:00 guard will be at {schedulePoint.Value.name}\n";
            _text += text;
        }

        _scheduleText.text = _text;
    }
}
