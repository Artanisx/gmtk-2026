using System;
using UnityEngine;

public class PlayerSabotage : MonoBehaviour
{
    [SerializeField] private float accepteDistance;
   
    private float CalculateDistance(GameObject player, GameObject machine)
    {
        var currentDistance = Vector3.Distance(player.transform.position, machine.transform.position);
        return currentDistance;
    }
    
    private void OperateMachine(CasinoMachine machine)
    {
        if (machine != null)
        {
            if (CalculateDistance(gameObject, machine.gameObject) <= accepteDistance)
            {
                machine.GetHacked();
            }
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        accepteDistance = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



public interface IActionCommand
{
    void DoAction();
}

public class SabotageCommand : IActionCommand
{
    
    
    public void DoAction()
    {
        throw new System.NotImplementedException();
    }
}