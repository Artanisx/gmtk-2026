using UnityEngine;

public class ConeOfView : MonoBehaviour
{
    // The player reference
    [SerializeField]
    private Transform player;
    
    private bool isPlayerInRange = false;
    
    // Check wheter the object colliding with the cone of view is the player 
    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true;
        }
    }
    
    // If the player exits the area (manages to escape) set it back to false 
    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }
    
    void Update ()
    {
        // If the player is in range check whether the guard can chase them
        if (isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    // player can be seen and a chase should start
                    Debug.Log("Player was seen!");
                    
                    // If we pass the Game Manager (or the player) object we can call for a function to start the chase
                }
            }
        }
    }
}
