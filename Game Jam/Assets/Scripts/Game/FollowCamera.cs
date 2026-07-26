using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;
    private CinemachineCamera cmCamera;
    private Camera mainCamera;

    void Awake()
    {
        // Get a refernece to the CinemachineCamera Component
        cmCamera  = GetComponent<CinemachineCamera>();
        
        // Get a refernece to the Main Camera present in the scene
        mainCamera = Camera.main;
    }
    void Start()
    {
        // Make sure the player has been set as target
        if (cmCamera.Target.TrackingTarget == null)
            Debug.LogError("CinemachineCamera target is not set! Please set the Player to it via the inspector.");
        
        // Make sure teh Main Camera has the required Cinemachine Brain component, if not add it
        if (mainCamera.GetComponent<CinemachineBrain>() == null)
        {
            Debug.Log("CinemachineBrain component is missing from the Main Camera. I'll add one myself, but for performance reasons it would be better to add one pre-run time.");
            
            // A CinemachineBrain component is not present in the camera, add one
            mainCamera.AddComponent<CinemachineBrain>();
        }
    }
}
