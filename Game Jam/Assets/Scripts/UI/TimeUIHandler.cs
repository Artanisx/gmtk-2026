using System;
using TMPro;
using UnityEngine;

public class TimeUIHandler : MonoBehaviour
{
    // not doing inherentence because why not, im lazy
    public GameObject GameSystemObject;
    private GameSystem gameSys;
    private TextMeshProUGUI textMesh;
    private TimeSpan timeSpan;
    private const string format = @"mm\:ss\:ff";
    private bool IsTimerGoing = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // getting the components
        gameSys = GameSystemObject.GetComponent<GameSystem>();
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }
    
    private void Update() => UpdateTime();

    private void UpdateTime()
    {
        if(!IsTimerGoing) return;
        
        timeSpan = TimeSpan.FromSeconds(gameSys.TimeSpent);
        ConvertTimeIntoText(textMesh, format);
    }

    private void ConvertTimeIntoText(TextMeshProUGUI textTMP, string format)
    {
        textTMP.text = timeSpan.ToString(format);
    }
}
