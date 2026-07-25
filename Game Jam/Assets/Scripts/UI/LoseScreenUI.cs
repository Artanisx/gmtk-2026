using Unity.VisualScripting;
using UnityEngine;

public class LoseScreenUI : MonoBehaviour
{
    [SerializeField] private LoadingUI menuLoadingScreen;
    [SerializeField] private LoadingUI levelLoadingScreen;
    private Animator animator;
    private AudioSource losingSound;
    private GameSystem gameSystem;

    void Awake()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // get component
        animator = gameObject.GetComponent<Animator>();
        losingSound = gameObject.GetComponent<AudioSource>();

        EventManager.PlayerLosed.AddListener(OnLoss);

        // hide lose screen
        gameObject.SetActive(false);
    }

    public void OnLoss()
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
        animator.Play("LoseFlash");
        losingSound.Play();
    }

    public void OnRetry()
    {
        levelLoadingScreen.TriggerLoading();
    }

    public void OnMenu()
    {
        menuLoadingScreen.TriggerLoading();
    }
}
