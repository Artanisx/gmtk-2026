using TMPro;
using UnityEngine;

public class TimeUIHandler : MonoBehaviour
{
    // not doing inherentence because why not, im lazy
    public GameObject GameSystemObject;
    private GameSystem gameSys;
    private TextMeshProUGUI textMesh;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // getting the components
        gameSys = GameSystemObject.GetComponent<GameSystem>();
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSpent = gameSys.TimeSpent;
        int minute = (int)(timeSpent/60);
        int second = (int)(timeSpent - 60 * minute);
        int millis = (int)((timeSpent - 60 * minute - second) * 1000);

        textMesh.text = $"{minute.ToString("000")}min {second.ToString("00")}sec {millis.ToString("000")}mil";
    }
}
