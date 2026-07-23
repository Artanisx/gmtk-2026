using System.IO;
using UnityEditor.VersionControl;
using UnityEngine;

public class NPCGuard : Character
{
    // This should be set by a Cone of View gameobject attached to the NPC Guard
    public bool IsPlayerInRange = false;
    
    // This should be set by a Cone of View gameobject attached to the NPC Guard, when the raycast hits the player
    public bool IsPlayerCaught = false;
    
    public bool MachineReached = false;
    
    public bool IsNPCChecking = false;
    
    public NPCGuard() : base(CharacterType.NPCGUARD)
    {
        
    }

    // This function will allow the NPC Guard to move in the next waypoint
    public override void HandleBehaviour()
    {
        base.HandleBehaviour();
        
        if (State == CharacterState.PATROL)
        {
            // Move the NPC Guard towards the next waypoint
            Patrol();
        }
        
        if (State == CharacterState.CHECK)
        {
            IsNPCChecking = true;
            
            // Check machine logic (timer etc)
            Check();
        }

        
    }

    private void Check()
    {
        // start the check
        // this will involve a timer of sort
        
        // finish the check
        IsNPCChecking = false;
        MachineReached = false;
    }

    public void Patrol()
    {
        // Patrol code
        
        // PATROL > CHASE - The player is seen
        if (IsPlayerInRange && State == CharacterState.PATROL)
            State = CharacterState.CHASE;      // NPC Guard will start chasing
        
        // CHASE > PATROL - The player escaped
        if (!IsPlayerInRange && State == CharacterState.CHASE)
            State = CharacterState.PATROL;
        
        // CHASE > ARREST - The player is caught
        if (IsPlayerCaught && State == CharacterState.CHASE)
            State = CharacterState.ARREST;      // NPC Guard will arrest the player (this will probably go to either a game over or something)
        
        // PATROL > CHECK - The guard reaches its next check waypoint
        if (MachineReached && State == CharacterState.PATROL)
            State = CharacterState.CHECK;      // NPC Guard will start checking the machine
        
        // CHECK > PATROL - The guard finishes the check and goes to the next check waypoint
        if (!IsNPCChecking && State == CharacterState.CHECK)
            State = CharacterState.PATROL;      // NPC Guard will start checking the machine
    }
}
