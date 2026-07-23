using UnityEngine;

public enum GameStatus
{
    LOSE,
    WON,
    PLAYING
}
public class GameSystem : MonoBehaviour
{
    public float CasinoMoney = 1000;

    [SerializeField]
    private GameStatus status = GameStatus.PLAYING;
    private double startingTime;
    private double finalTimeSpent = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // setting up starting time
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // check for win condition
        if (CasinoMoney <=  0 && status == GameStatus.PLAYING)
        {
            status = GameStatus.WON;
            finalTimeSpent = Time.realtimeSinceStartupAsDouble - startingTime;
        }
    }

    // Always use this function to handle game status.
    // Make player lose
    public void SetLosingStatue()
    {
        if (status != GameStatus.PLAYING) return;

        status = GameStatus.LOSE;
        finalTimeSpent = Time.realtimeSinceStartupAsDouble - startingTime;
    }

    // Make Player win
    public void SetWinningStatue()
    {
        if (status != GameStatus.PLAYING) return;

        status = GameStatus.WON;
        finalTimeSpent = Time.realtimeSinceStartupAsDouble - startingTime;
    }

    // Use it to reset timer and put player on playing mode
    public void RestartGame()
    {
        startingTime = Time.realtimeSinceStartup;
        status = GameStatus.PLAYING;
    }

    public double TimeSpent
    {
        get
        {
            // if player has finish, we shouldn't continue to count time
            if (status == GameStatus.PLAYING) return Time.realtimeSinceStartupAsDouble - startingTime;
            return finalTimeSpent;
        }
    }

    public GameStatus Status
    {
        get {return status;}
    } 
}
