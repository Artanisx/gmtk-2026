using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSabotage : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InputActionReference inputSabotageRef;
    [SerializeField] private CasinoMachine targetMachine;
    
    [SerializeField] private float sabotageMinimumDistance;
    [SerializeField] private float rayCameraDistance;
    
    [SerializeField] private GameObject hackingMinigamePrefab;
    
    private GameSystem gameSystem;
    
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
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        
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
        // THere should be amachine being selected, the minigame should not be already invoked and the status should be playing (not won or lose)
        if (machine != null && gameSystem.IsMinigameSpawned == false && gameSystem.Status == GameStatus.PLAYING)
        {
            // Instantiate the Hacking Minigame
            var miniGame = Instantiate(hackingMinigamePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            
            // Disable it so we can set it up properly first
            miniGame.SetActive(false);
            
            // Get a reference to its script
            HackMinigameInput hackingMinigame = miniGame.GetComponent<HackMinigameInput>();
            
            // Set the minigame up to start immediately once we enable it
            hackingMinigame.AutoStartOnEnable = true;
            
            // Set the minigame difficulty based on the amount of money available in the machine
            hackingMinigame.SetDifficulty(machine.GetAmountOfMoneyToSteal());
            
            // Add to the OnSuccess event the steps to properly sabotage the machine and conclude the minigame
            hackingMinigame.OnSuccess.AddListener(machine.GetSabotaged);
            hackingMinigame.OnSuccess.AddListener(hackingMinigame.DestroyMinigame);
            hackingMinigame.OnSuccess.AddListener(gameSystem.ResetMinigame);
            
            // Add to the OnFailure event to properly handle failure
            hackingMinigame.OnFailure.AddListener(hackingMinigame.Retry);

            // Enable the minigame
            miniGame.SetActive(true);

            // To avoid spawning multiple minigames
            gameSystem.IsMinigameSpawned = true;
        }
    }
    
}