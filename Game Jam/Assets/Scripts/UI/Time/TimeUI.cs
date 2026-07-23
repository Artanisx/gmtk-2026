using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    
    private void Awake() => EventManager.TimeHasChanged.AddListener(UpdateTime);

    private void UpdateTime(int currentHour) => _timerText.text = $"{currentHour}:00";
}
