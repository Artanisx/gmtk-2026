using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private bool makeInitialAnimation;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int sceneId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        if (makeInitialAnimation) animator.Play("SlideOut");
    }

    // Change scene
    public void TriggerLoading() => StartCoroutine(TriggerLoading(sceneId));

    private IEnumerator TriggerLoading(int sceneId)
    {
        animator.Play("SlideIn");

        yield return new WaitForSeconds(1);

        // load scene in the background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        // while the scene load we update the progress bar
        while (!asyncLoad.isDone)
        {
            // check if it has finished

            yield return new WaitForEndOfFrame();
        }
    }
}
