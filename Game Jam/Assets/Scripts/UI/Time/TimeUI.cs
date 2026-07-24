using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    
    private void Awake() => EventManager.TimeHasChanged.AddListener(UpdateTime);

    private void UpdateTime(int currentHour, int currentMinute) => _timerText.text = $"{currentHour}:{currentMinute.ToString("00")}";
}
