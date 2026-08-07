using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPCLoseCondition : MonoBehaviour
{
    private GameSystem gameSystem;
    public float checkingRadius = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find gamesys
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();

        // if not found raise error
        if (gameObject == null)
        {
            Debug.LogError("GameSystem not found, please add it");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // We check if any object is close to guard
        Collider[] colliders;

        colliders =  Physics.OverlapSphere(transform.position, checkingRadius);
        
        foreach(Collider collider in colliders)
        {
            // check if object is a player
            GameObject gameObject = collider.gameObject;
            
            if (gameObject.tag != "Player") continue;

            AudioManager.MusicInstance.setParameterByNameWithLabel("Alert", "True");
            gameSystem.SetLosingStatue();
        }
    }
}
