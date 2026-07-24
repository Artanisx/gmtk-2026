using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScheduleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scheduleText;
    private string _text = "";
    
    private void Awake()
    {
        foreach (var schedulePoint in NPCScheduleManager.Instance.ScheduledWaypoints.OrderBy(pair => pair.Key))
        {
            var text = $"At {schedulePoint.Key}:00 guard will be at {schedulePoint.Value.name}\n";
            _text += text;
        }

        _scheduleText.text = _text;
    }
}
