using System;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class HackMinigameInput : MonoBehaviour
{
    public enum State { IDLE, WAITING, HACKING, HACKRESULT}

    [Header("GO References")] 
    public RectTransform HackBar;
    public RectTransform PlayerLine;
    public RectTransform SuccessArea;
    public TMP_Text Message;

    [Header("Minigame Settings")] 
    [Tooltip("The speed of the Player Line; Higher speed makes it more difficult.")]
    public float PlayerLineSpeed = 1.5f;
    [Tooltip("Delay for the minigame round start.")]
    public Vector2 StartWaitRange = new Vector2(0.75f, 1.75f);
    [Tooltip("Set wheter the success area will appear in randomized places.")]
    public bool RandomizedStartZone = true;
    [Tooltip("Set the size of the Success Zone; Bigger green makes it easier.")]
    public Vector2 SuccessZoneRange = new Vector2(0.18f, 0.32f);
    [Tooltip("Where the zone can appear.")]
    public Vector2 SuccessZoneCenterClamp = new Vector2(0.15f, 0.85f);
    [Tooltip("Should the minigame start automatically?")]
    public bool AutoStartOnEnable = true;
    
    [Header("Input Settings")]
    [Tooltip("InputActionReference for the stop button action. The player uses this button to stop the Player Line")]
    public InputActionReference StopAction;
    
    [Header("Unity Events")]
    [Tooltip("Event that should happen when the hack is successful; this can be a particle effect, a SFX or the direct success of the hacking minigame.")]
    public UnityEvent OnSuccess;
    [Tooltip("Event that should happen when the hack is unsuccessful; for example closing the minigame or starting it again.")]
    public UnityEvent OnFailure;

    // Private variables
    private State state = State.IDLE;
    private float posAlongTheLine; // range 0 to 1 along the line
    private int direction = 1; // -1 down +1 up
    private float waitTimer;    // How long before the PlayerLine should star moving

    // When the minigame prefab is enabled...
    private void OnEnable()
    {
        // First, make sure the StopAction reference has been set and it's configured 
        if (StopAction != null && StopAction.action != null)
        {
            StopAction.action.performed += OnStopPerformed; // Callback
            StopAction.action.Enable();
        }
        
        // If autostart is set, then we immediately start
        if (AutoStartOnEnable)
            StartHacking();
    }

    // Minigame is over
    private void OnDisable()
    {
        if (StopAction != null && StopAction.action != null)
        {
            // We disable the action because we don't want it to interfere with regular game
            StopAction.action.performed -= OnStopPerformed; // Removal of the Callback
            StopAction.action.Disable();
        }
    }
    
    // Handles everything
    private void Update()
    {
        switch (state)
        {
            case State.WAITING:
                // We start the countdown (eheh) before the minigame begins
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0.0f)
                {
                    // transition to running state
                    state = State.HACKING;
                    
                    // Reset the text (if it exists)
                    if (Message)
                        Message.text = string.Empty;
                }

                break;
            case State.HACKING:
                UpdatePlayerLine();
                break;
        }
    }
    
    // CALLBACK For STOP ACTION
    private void OnStopPerformed(InputAction.CallbackContext obj)
    {
        // We respond to this action ONLY if the hack is in progress
        if (state == State.HACKING)
        {
            Evaluate();
        }
    }

    // PREPARES AN HACKING ROUND
    private void StartHacking()
    {
        // Check if all references and text is ready
        if (!ValidateRefs())
            return;

        if (Message)
            Message.text = String.Empty;
        
        // If the minigame is set to randomize the success zone, do so
        if (RandomizedStartZone)
            RandomizeZone();
        
        // Randomize the starting position of the player line
        posAlongTheLine = Random.Range(0.05f, 0.95f);
        
        // Select a random direction for the start of the Player Line movement 50% it's up 50% it's down 
        direction = Random.value > 0.5f ? 1 : -1;

        // Apply the calculated position of the player line along the bar
        ApplyPlayerLinePosition();
        
        // Finally, set the timer for the next round (random between the set range)
        waitTimer = Random.Range(StartWaitRange.x, StartWaitRange.y);
        
        // Set the state to waiting for the next round
        state = State.WAITING;
    }

  


    // If the player wants to cancel the hacking attempt (i.e. a guard is coming)
    public void CancelHacking()
    {
        state = State.WAITING;
        if (Message)
            Message.text = string.Empty;
    }

    // This function moves the player line along the bar
    private void UpdatePlayerLine()
    {
        posAlongTheLine += direction * PlayerLineSpeed * Time.deltaTime;
        
        // if the line reaches one end...
        if (posAlongTheLine >= 1f)
        {
            // max it out to 1f;
            posAlongTheLine = 1f;
            
            // flips it 
            direction = -1;
        }
        // or the other...
        else if (posAlongTheLine < 0)
        {
            // max it out to 0f;
            posAlongTheLine = 0f;
            
            // flips it 
            direction = 1;
        }
        
        // Apply the calculated position of the player line along the bar
        ApplyPlayerLinePosition();
    }

    // Updates the actual position of the player line
    private void ApplyPlayerLinePosition()
    {
        // Hackbar and playuer line references must be set
        if (!HackBar || !PlayerLine)
            return;

        float yPosition = Mathf.Lerp(GetHackBarBottom(), GetHackBarTop(), posAlongTheLine);
        
        var pos = PlayerLine.anchoredPosition;
        pos.y = yPosition;      // we only move it vertically
        PlayerLine.anchoredPosition = pos;
    }
    
    // Check if the player successfully pressed the action when the line is in the successzone
    private void Evaluate()
    {
        state = State.HACKRESULT;

        bool success = IsPlayerLineInsideZone();

        
        if (success)
        {
            if (Message)
                Message.text = "HACK SUCCEEDED!";
            
            // Invoke the OnSuccess event
            OnSuccess?.Invoke();
        }
        else
        {
            if (Message)
                Message.text = "HACK FAILED!";
            
            // Invoke the OnFailure event
            OnFailure?.Invoke();
        }
    }

    // Checks if the playerline is inside the success zone
    private bool IsPlayerLineInsideZone()
    {
        // only if the references are properly set
        if (!PlayerLine || !SuccessArea)
            return false;
        
        // Set the success area variables
        float playerLineY = PlayerLine.anchoredPosition.y;
        float areaHalfHeight = SuccessArea.rect.height * 0.5f;
        float areaCenter = SuccessArea.anchoredPosition.y;
        float areaMin = areaCenter - areaHalfHeight;
        float areaMax = areaCenter + areaHalfHeight;
        
        // return wheter the playerline is inside the success area or not
        return playerLineY >= areaMin && playerLineY <= areaMax;
    }
    
    // Randomizes the success zone
    private void RandomizeZone()
    {
        // only if the references are properly set
        if (!HackBar || !SuccessArea)
            return;
        
        float areaH = HackBar.rect.height;
        float zoneFraction = Random.Range(SuccessZoneRange.x, SuccessZoneRange.y);
        
        // make sure the zone isn't too small or too big
        float zoneH = Mathf.Clamp(zoneFraction, 0.05f, 0.9f) * areaH;
        
        float minCenter = Mathf.Lerp(GetHackBarBottom(), GetHackBarTop(), SuccessZoneCenterClamp.x);
        float maxCenter = Mathf.Lerp(GetHackBarBottom(), GetHackBarTop(), SuccessZoneCenterClamp.y);
        
        // Select a random center
        float centerY = Random.Range(minCenter, maxCenter);
        
        var size = SuccessArea.sizeDelta;
        size.y = zoneH; // only y changes
        SuccessArea.sizeDelta = size;
        
        var pos = SuccessArea.anchoredPosition;
        pos.y = Mathf.Lerp(centerY, GetHackBarBottom() + zoneH * 0.5f, GetHackBarTop() - zoneH * 0.5f);
    }

    
    // Calculates the bottom area of the HackBar
    private float GetHackBarBottom()
    {
        return -HackBar.rect.height * 0.5f;
    }
    
    // Calculates the top area of the HackBar
    private float GetHackBarTop()
    {
        return HackBar.rect.height * 0.5f;
    }
    
    // Sends a console error message if references are not set
    private bool ValidateRefs()
    {
        if (!HackBar || !SuccessArea || !PlayerLine)
        {
            Debug.LogError("[Hack Minigame] Missing references. Assign HackBar, SuccessArea and PlayerLine references!");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void PressStop() => OnStopPerformed(default);
    public void Retry() => StartHacking();
}
