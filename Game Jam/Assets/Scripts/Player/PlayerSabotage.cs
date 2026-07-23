using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSabotage : MonoBehaviour
{
    [SerializeField] private InputActionReference inputSabotageRef;
    [SerializeField] public float accepteDistance;
    [SerializeField] private CasinoMachine targetMachine;
    
    // On script being enabled fetch for the proper input action delegate and assigns it..
    public void OnEnable()
    {
        inputSabotageRef.action.performed += OnSabotagingMachine;
        inputSabotageRef.action.Enable();
    }
    
    // On Awake setup Target Machine to null..
    // This is a reference to a variable that will be used as a "single target" option
    public void Awake()
    {
        targetMachine = null;   
    }

    // On script being disabled removes for the existing input action delegate assigned to it..
    private void OnDisable()
    {
        inputSabotageRef.action.performed -= OnSabotagingMachine;
        inputSabotageRef.action.Disable();
    }
    
    // Sets the targetMachine to be equal to the one that's passed as parameter
    public void SetTargetMachine(CasinoMachine machine)
    {
        Debug.Log("[Machine is being targeted.. setting value]");
        targetMachine = machine;
    }
    
    //INPUT -> Pressing the Button ("F")
    //Input can be changed to be whatever button we want
    public void OnSabotagingMachine(InputAction.CallbackContext context)
    {
        //Clicked on button !
        TrySabotageMachine(targetMachine);
    }
    
    //The coup-de-grace!
    // This is where it sets the machine if there's a machine value on the variable of type <CasinoMachine>
    // If there is, calculates if there's within the distance to it.. can be wired in the editor that distance as we please
    // If it does, then it hacks the machine..
    private void TrySabotageMachine(CasinoMachine machine)
    {
        Debug.Log("[Sabotage Starting..]");
        if (machine != null)
        {
            Debug.Log("[Machine exists");
            if (CalculateDistance(gameObject, machine.gameObject) <= accepteDistance)
            {
                Debug.Log("[It's within distance..]");
                machine.GetSabotaged();
            }
        }
    }
    
    //Calculats the distance between 2 game objects
    //Not sure we needed this method xD
    private float CalculateDistance(GameObject player, GameObject machine)
    {
        var currentDistance = Vector3.Distance(player.transform.position, machine.transform.position);
        return currentDistance;
    }
}