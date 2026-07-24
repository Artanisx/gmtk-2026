using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSabotage : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InputActionReference inputSabotageRef;
    [SerializeField] private CasinoMachine targetMachine;
    
    [SerializeField] private float sabotageMinimumDistance;
    [SerializeField] private float rayCameraDistance;

    
    // On script being enabled fetch for the proper input action delegate and assigns it..
    public void OnEnable()
    {
        inputSabotageRef.action.performed += OnInputCallSabotage;
        inputSabotageRef.action.Enable();
    }
    
    // On Awake setup Target Machine to null..
    // This is a reference to a variable that will be used as a "single target" option
    public void Awake()
    {
        targetMachine = null;   
        playerCamera = Camera.main;
    }

    // On script being disabled removes for the existing input action delegate assigned to it..
    private void OnDisable()
    {
        inputSabotageRef.action.performed -= OnInputCallSabotage;
        inputSabotageRef.action.Disable();
    }
    
    public void SendRaycastTowardsMachine()
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * rayCameraDistance, Color.red);           
        if (Physics.Raycast(ray, out hit, rayCameraDistance, LayerMask.GetMask("Machine")))
        {
            var distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance <= sabotageMinimumDistance)
            {
                targetMachine = hit.transform.gameObject.GetComponent<CasinoMachine>();
                TrySabotageMachine(targetMachine);
            }
        }
    }

    public void TestFunction()
    {
        // Vector3 adjustedHeightPoint = new Vector3(hit.point.x,transform.position.y,hit.point.x);
        // Vector3 directionToTarget = adjustedHeightPoint - transform.position;
        // Vector3 finalDirectionToTarget = directionToTarget.normalized * this.sabotageMinimumDistance;
    }
    
    public void OnInputCallSabotage(InputAction.CallbackContext context)
    {
        //Clicked on button !
        Debug.Log("Input is " + context.ReadValueAsButton());
        SendRaycastTowardsMachine();
        // TrySabotageMachine(targetMachine);
    }
    
    // Sets the targetMachine to be equal to the one that's passed as parameter
    public void SetTargetMachine(CasinoMachine machine)
    {
        Debug.Log("[Machine is being targeted.. setting value]");
        targetMachine = machine;
    }
    
    //INPUT -> Pressing the Button ("F")
    //Input can be changed to be whatever button we want

    
    //The coup-de-grace!
    // This is where it sets the machine if there's a machine value on the variable of type <CasinoMachine>
    // If there is, calculates if there's within the distance to it.. can be wired in the editor that distance as we please
    // If it does, then it hacks the machine..
    private void TrySabotageMachine(CasinoMachine machine)
    {
        if (machine != null)
        {
            machine.GetSabotaged();
        }
    }
    
}