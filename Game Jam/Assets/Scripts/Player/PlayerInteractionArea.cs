using System;
using UnityEngine;

public class PlayerInteractionArea : MonoBehaviour
{
    public PlayerSabotage playerSabotage;
    public Action<CasinoMachine> OnTargetSet;
    
    // Sets the Collider bounds/radius to be increased/decreased according to the acceptedDistance variable
    // on the parent gameobject class PlayerSabotage..
    // Collisions aren't being detected for the purpose of setting up targets, i'm using Trigger instead..
    private void SetTriggerInteractionArea(float distance)
    {
        var sphereCollider = gameObject.GetComponent<SphereCollider>();
        sphereCollider.radius = distance;
        Debug.Log("[Set Radius based on distance accepted to interact]");
    }
    
    
    // On Awake, setup delegate for targeting machine function..
    // Also set radius of Trigger Area..
    public void Awake()
    {
        OnTargetSet += playerSabotage.SetTargetMachine;
        SetTriggerInteractionArea(playerSabotage.accepteDistance);
        Debug.Log("[Setup delegate for Targeting of machine]");
    }

    // When "triggering" OnTriggerEnter, checks if it's a machine with the Tag..
    // If this tag is inexistent we need to add it or another one that it can match with it..
    // If it manages to compareTag() then proceeds to set the machine target on PlayerSabotage through the delegate invokation
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Triggering OnTriggerEnter]");
        if (other.CompareTag("Machine"))
        {
            Debug.Log("[On TriggerEnter Against <Machine>]");
            var machine = other.GetComponent<CasinoMachine>();
            OnTargetSet.Invoke(machine);
        }
    }
    
    // Same thing as above but
    // If it manages to compareTag() then proceeds to set the machine target on PlayerSabotage
    // through the delegate invokation with a null value
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("[Triggering OnTriggerExit]");
        if (other.CompareTag("Machine"))
        {
            Debug.Log("[On TriggerExit Against <Machine>]");
            var machine = other.GetComponent<CasinoMachine>();
            machine = null;
            OnTargetSet.Invoke(machine);
        }
    }
}