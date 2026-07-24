using System.Collections;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        StartCoroutine(TriggerLoading(1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TriggerLoading(int sceneId)
    {
        yield return new WaitForSeconds(1);

        animator.Play("SlideIn");

        while (animator.GetAnimatorTransitionInfo(0).normalizedTime > 1.0f) yield return null;

        yield return new WaitForSeconds(4);

        animator.Play("SlideOut");
    }
}
