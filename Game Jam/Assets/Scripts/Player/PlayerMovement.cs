using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Camera PlayerCamera;
    public float CameraHeight = 10;
    public float Speed = 5;
    private Vector2 playerDirection;
    private CharacterController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        // move  player
        controller.Move(new Vector3(playerDirection.x,-9,playerDirection.y) * Speed * Time.deltaTime);

        // setup camera
        PlayerCamera.transform.position = transform.position + Vector3.up * CameraHeight;
        PlayerCamera.transform.rotation.SetLookRotation(Vector3.down,Vector3.forward);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // updating of player direction
        playerDirection = context.ReadValue<Vector2>();
    }
}
