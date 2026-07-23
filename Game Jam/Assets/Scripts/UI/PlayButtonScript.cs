using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    public void OnPlay()
    {
        StartCoroutine(LoadScene(1));
    }

    public IEnumerator LoadScene(int index)
    {
        // load scene in the background
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        // while the scene load we update the progress bar
        while (!asyncLoad.isDone)
        {
            // check if it has finished
            if (asyncLoad.progress > 0.9f) asyncLoad.allowSceneActivation = true;

            yield return null;
        }
    }
}
