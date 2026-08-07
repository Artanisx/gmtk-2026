using System;
using Unity.Mathematics;
using UnityEngine;

public class ConeOfView : MonoBehaviour
{
    // The player reference
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float detectionRange;
    [SerializeField]
    private float angleOfView;
    
    private bool isPlayerInRange = false;
    private NPCGuardController controller;

    private void Awake()
    {
        // load component
        controller = gameObject.GetComponent<NPCGuardController>();
    }

    void Update ()
    {
        // debug stuff
        Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(angleOfView/2, transform.up) * transform.forward * detectionRange);
        Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(-angleOfView/2, transform.up) * transform.forward * detectionRange);
        
        // we first do a sphere overlap to check all objects in the proximity of the guard
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        // next we check if the player is closed to the guard
        isPlayerInRange = false;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetEntityId() == player.GetEntityId())
            {
                isPlayerInRange = true;
                break;
            }
        }

        // if it's the case, we now see if the player is in the cone of view and no obstacle is blocking the view
        if (isPlayerInRange)
        {
            // We calculate ray info
            Vector3 direction = player.transform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            // We do the same for the angle of view
            float angle = math.acos(Vector3.Dot(direction.normalized, transform.forward));
            angle = math.degrees(angle);

            // we don't check the angle in chase status
            if((angle <= angleOfView/2 || controller.npcGuard.State == CharacterState.CHASE) && Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.gameObject.GetEntityId()== player.GetEntityId())
                {
                    // player can be seen and a chase should start
                    controller.npcGuard.IsPlayerInRange = true;
                    EventManager.NotifyPlayerWasSeen(true);
                    AudioManager.MusicInstance.setParameterByNameWithLabel("Alert", "True");
                    return;

                    // If we pass the Game Manager (or the player) object we can call for a function to start the chase
                }
            }
        }

        controller.npcGuard.IsPlayerInRange = false;
        AudioManager.MusicInstance.setParameterByNameWithLabel("Alert", "False");
    }

    public float AngleOfView
    {
        get {return angleOfView;}
        set {angleOfView = value;}
    }
}
