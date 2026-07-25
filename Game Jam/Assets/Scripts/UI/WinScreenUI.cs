using UnityEngine;

public class WinScreenUI : MonoBehaviour
{
    [SerializeField] private LoadingUI menuLoadingScreen;
    private Animator _animator;
    private AudioSource _winSound;
    private GameSystem gameSystem;
    
    private void Awake()
    {
        // get component
        _animator = gameObject.GetComponent<Animator>();
        _winSound = gameObject.GetComponent<AudioSource>();

        EventManager.PlayerWin.AddListener(OnWin);

        // hide win screen
        gameObject.SetActive(false);
        
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
    }

    private void OnWin()
    {
        // Check if the minigame was running, if so destroy it
        if (gameSystem.IsMinigameSpawned)
        {
            HackMinigameInput hackingMinigame = GameObject.Find("Hacking Minigame(Clone)").GetComponent<HackMinigameInput>();
            if (hackingMinigame == null)
                Debug.LogError("I couldn't find the minigame object even though it is running.");
            hackingMinigame.DestroyMinigame();
        }
        
        gameObject.SetActive(true);
    }
    
    public void OnMenu()
    {
        menuLoadingScreen.gameObject.SetActive(true);
        menuLoadingScreen.TriggerLoading();
    }
}
