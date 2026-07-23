using UnityEngine;

// Basic Class for all Characters in the game (Player, NPCs)
// It includes basic states and variables

public enum CharacterType
{
    PLAYER,
    NPCSTAFF,
    NPCGUARD
}

public enum CharacterState
{
    IDLE,           // IDLE STATE, USEFUL FOR PLAY IDLE ANIMATION
    WALK,           // WALK STATE, USED FOR THE PLAYER AND RELATED ANIMATION
    HACK,           // HACK STATE, USED FOR THE PLAYER TO HACK A MACHINE
    PATROL,         // PATROL STATE, USED FOR THE NPC GUARD TO MOVE AROUND WAYPOINTS 
    CHASE,          // CHASE STATE, USED FOR THE NPC GUARD TO CHASE THE PLAYER AROUND THE MAP
    CHECK,          // CHECK STATE, USED FOR THE NPC GUARD TO CHECK THE MACHINE FOR TAMPERING
    ARREST,         // ARREST STATE, USED FOR THE NPC GUARD TO ARREST THE PLAYER ONCE THEY CAUGHT THEM
    WORK            // WORK STATE, USED FOR THE NPC STAFF TO MAN THE MACHINE (DEALING CARDS etc)
}
 

public class Character
{
    public CharacterType Type;
    public CharacterState State = CharacterState.IDLE;
    
    public float WalkSpeed;
    public float RunSpeed;
    
    public Character(CharacterType characterType)
    {
        Type = characterType;
    }
    
    // Handles movement for this character (state changes)
    public void HandleMovement()
    {
        if (CanMove())
        {
            if (Type == CharacterType.NPCGUARD)
                State = CharacterState.PATROL;      // NPC Guard will start patrolling
            else if (Type == CharacterType.NPCSTAFF)
                State = CharacterState.WORK;        // Staff can only work at their place 
        }
    }
    
    // Function will return wheter the character can move (the correct states that allow it)
    public bool CanMove()
    {
        return State == CharacterState.IDLE || State == CharacterState.PATROL || State == CharacterState.WALK;
    }
    
    // Function to handle input, this will either handle player movement with keyboard/mouse or make the npc move on their own
    public virtual void HandleInput() {}
    
}
