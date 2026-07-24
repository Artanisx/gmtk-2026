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
    }

    public void OnLoss()
    {
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
