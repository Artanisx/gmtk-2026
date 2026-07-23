using UnityEngine;

public class NPCGuardController : MonoBehaviour
{
    // Create a NPCGuard
    private NPCGuard npcGuard;
    
    // This will hold a reference to the player transform (used for chase)
    public Transform player;
    
    void Start()
    {
        // Create a NPC Guard
        npcGuard = new NPCGuard();

        // Set its start state to IDLE
        npcGuard.State = CharacterState.IDLE;
    }

    void Update()
    {
        npcGuard.HandleInput();
        npcGuard.HandleMovement();
    }
    
}
