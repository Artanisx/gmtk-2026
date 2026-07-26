using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionButtonScript : MonoBehaviour
{
    public LoadingUI loadingUI;
    
    [Tooltip("The scene id of the Help Scene. This will be loaded when the player clicks this button.")]
    public int HelpSceneID = 4;
    public void OnInstructions()
    {
        loadingUI.SetSceneID(HelpSceneID);
        loadingUI.TriggerLoading();
    }
}
