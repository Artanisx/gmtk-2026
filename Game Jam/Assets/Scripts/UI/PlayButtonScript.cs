using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    public LoadingUI loadingUI;
    
    [Tooltip("The scene id of the Play Scene. This will be loaded when the player clicks this button.")]
    public int PlaySceneID = 3;
    
    public void OnPlay()
    {
        loadingUI.SetSceneID(PlaySceneID);
        loadingUI.TriggerLoading();
    }
}
