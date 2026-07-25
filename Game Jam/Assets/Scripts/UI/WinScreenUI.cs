using UnityEngine;

public class WinScreenUI : MonoBehaviour
{
    [SerializeField] private LoadingUI menuLoadingScreen;
    private Animator _animator;
    private AudioSource _winSound;
    
    private void Awake()
    {
        // get component
        _animator = gameObject.GetComponent<Animator>();
        _winSound = gameObject.GetComponent<AudioSource>();

        EventManager.PlayerWin.AddListener(OnWin);

        // hide win screen
        gameObject.SetActive(false);
    }

    private void OnWin()
    {
        gameObject.SetActive(true);
    }
    
    public void OnMenu()
    {
        menuLoadingScreen.TriggerLoading();
    }
}
