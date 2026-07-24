using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private int sceneId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        animator.Play("SlideOut");
    }

    // Change scene
    public void TriggerLoading() => StartCoroutine(TriggerLoading(sceneId));

    private IEnumerator TriggerLoading(int sceneId)
    {
        yield return new WaitForSeconds(1);

        animator.Play("SlideIn");

        // load scene in the background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);
        asyncLoad.allowSceneActivation = false;

        yield return new WaitForSeconds(1);

        // while the scene load we update the progress bar
        while (!asyncLoad.isDone)
        {
            // check if it has finished

            if (asyncLoad.progress >= 0.9f) asyncLoad.allowSceneActivation = true;

            yield return null;
        }
    }
}
