using Unity.VisualScripting;
using UnityEngine;

public class LoseScreenUI : MonoBehaviour
{
    [SerializeField] private LoadingUI menuLoadingScreen;
    [SerializeField] private LoadingUI levelLoadingScreen;
    private Animator animator;
    private AudioSource losingSound;
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
