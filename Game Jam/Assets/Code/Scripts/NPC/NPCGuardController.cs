using UnityEngine;

public class NPCGuardController : MonoBehaviour
{
    // Create a NPCGuard
    NPCGuard npcGuard;
    
    void Start()
    {
        npcGuard = new NPCGuard();
        
           Debug.Log(npcGuard.Type);
    }
}
