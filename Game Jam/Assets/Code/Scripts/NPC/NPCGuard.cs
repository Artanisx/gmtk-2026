using UnityEngine;

public class NPCGuard : Character
{
    public NPCGuard() : base(CharacterType.NPCGUARD)
    {
        
    }

    // This function will allow the NPC Guard to move in the next waypoint
    public override void HandleInput()
    {
        base.HandleInput();
        
        if (CanMove())
        {
            // Move the NPC Guard towards the next waypoint
        }
        
    }
}
