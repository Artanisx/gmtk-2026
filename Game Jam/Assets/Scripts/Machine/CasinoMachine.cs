using UnityEngine;

public class CasinoMachine : MonoBehaviour
{
    // boolean to decide if it's hacked or not.
    public bool isMachineHacked;

    // basic enum for states since there's only 2 and doesn't need to have more functionality like player, guards,etc
    public enum ECasinoMachineState
    {
        Working,
        Broken,
    }
    
    private ECasinoMachineState myState;
    
    // Maybe we can use Events for simplify the communication between entities? Not doing it for now..
    // But the machine decides it's own state instead of being done in the PlayerSabotage script..
    // Will also help with when something happens -> sends message to other systems (AI,UI,etc)
    public void GetHacked()
    {
        isMachineHacked = true;
        SetMachineState(ECasinoMachineState.Working);
        // Perform some more operations here 
    }

    public void GetFixed()
    {
        isMachineHacked = false;
        SetMachineState(ECasinoMachineState.Broken);
    }
    
    public void SetMachineState(ECasinoMachineState state)
    {
        if (state == ECasinoMachineState.Working)
        {
            // 
        }
        else
        {
            // Probably signal "Guard/AI" to come check it? 
        }
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMachineHacked = false;
        myState =  ECasinoMachineState.Working;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
