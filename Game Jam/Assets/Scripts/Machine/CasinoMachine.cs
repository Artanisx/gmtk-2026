using UnityEngine;

public class CasinoMachine : MonoBehaviour
{
    // Testing material
    public Material stateMaterial;
    
    //Enum of machine states
    public enum ECasinoMachineState
    {
        Working,
        Broken,
    }
    
    // basic enum for states since there's only 2 and doesn't need to have more functionality like player, guards,etc
    [SerializeField] ECasinoMachineState myState;
    
    // Maybe we can use Events for simplify the communication between entities? Not doing it for now..
    // But the machine decides it's own state instead of being done in the PlayerSabotage script..
    
    //Depending on it's state either get's sabotaged or fixed
    public void GetSabotaged()
    {
        if (myState == ECasinoMachineState.Working)
        {
            GetBroken();
        }
        else
        {
            GetFixed();   
        }
    }

    //Get broken will make it start to countdown and get money from the casino
    //KASHING!
    public void GetBroken()
    {
        SetMachineState(ECasinoMachineState.Broken);

        Debug.Log("My state is: " + myState);
    }

    //Get Fixed will make it stop counting down from this pool of money
    //ohhhh!
    public void GetFixed()
    {
        SetMachineState(ECasinoMachineState.Working);
        
        Debug.Log("My state is: " + myState);

    }
    
    // Not even a state machine, just an enum for having 2 different states
    // A bit hardcoded, but sets itself the state based on the parameter state.. quite obvious
    // it can also do some internal stuff (UI,VFX,SOUND,etc) later on..
    public void SetMachineState(ECasinoMachineState state)
    {
        if (state == ECasinoMachineState.Working)
        {   
            myState = ECasinoMachineState.Working;
            GetComponent<MeshRenderer>().material = stateMaterial;
            stateMaterial.color = Color.blue;
            // Does something here? 
            // Update UI, Art?
        }
        else
        {
            myState = ECasinoMachineState.Broken;
            GetComponent<MeshRenderer>().material = stateMaterial;
            stateMaterial.color = Color.darkRed;
            // Probably signal "Guard/AI" to come check it? 
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMachineState(ECasinoMachineState.Working);
        Debug.Log("My state is: " + myState);
    }

    public ECasinoMachineState MyState
    {
        get {return myState;}
    }
}
