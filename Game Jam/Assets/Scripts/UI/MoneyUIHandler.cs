using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MoneyUIHandler : MonoBehaviour
{
    // Sorry if the name are completly dumb
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
        // use it to update the money show
        float amout = gameSys.CasinoMoney;
        string moneySimplified; // yeah I didn't have any imagination for this one

        // We get the number of digit so we can show the money without taking all the screen
        int numDigit = (int)math.log10(amout);
        
        // according to the number of digit we show the money in the format (100, 42K, 1B)
        if (numDigit < 3) moneySimplified = ((int)amout).ToString();
        else if (numDigit < 6) moneySimplified = ((int)(amout / 1000)).ToString() +  "K";
        else if (numDigit < 9) moneySimplified = ((int)(amout / 1000000)).ToString() +  "M";
        else moneySimplified = ((int)(amout / 1000000000)).ToString() +  "B";

        // and now we update text
        textMesh.text = $"Money left : {moneySimplified} $";
    }
}
