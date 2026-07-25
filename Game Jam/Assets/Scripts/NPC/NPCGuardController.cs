using System;
using System.Reflection.Metadata;
using NUnit.Framework.Constraints;
using UnityEngine;

public class NPCGuardController : MonoBehaviour
{
    // Create a NPCGuard
    public NPCGuard npcGuard;

    // This will hold a reference to the player transform (used for chase)
    public Transform player;

    private void Awake()
    {
        //EventManager.TimeHasChanged.AddListener(ScheduledPatrol);
    }

    void Start()
    {
        // Create a NPC Guard
        npcGuard = new NPCGuard();

        // Set its start state to IDLE
        npcGuard.State = CharacterState.PATROL;
    }

    void Update()
    {
        //npcGuard.HandleMovement();
        npcGuard.HandleBehaviour();
        
    }
    
    
    private void ScheduledPatrol(int hour, int minute)
    {
        npcGuard.State = CharacterState.PATROL;
    }
}
