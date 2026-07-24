using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    public LoadingUI loadingUI;
    public void OnPlay()
    {
        loadingUI.TriggerLoading();
    }
}
