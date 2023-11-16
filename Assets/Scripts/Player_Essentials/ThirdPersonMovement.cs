using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public PlayerActions controls;
    public GameObject canvasObject;

    public float speed = 6f;
    public float jumpSpeed = 1f;
    public float gravityValue = -9.81f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private bool groundedPlayer;

    private Vector3 _playerVelocity;

    private void Awake()
    {
        controls = new PlayerActions();
        //controls.PlayerMain.Move.performed += _ => WeMove();
        //controls.PlayerMain.Look.performed += _ => WeLook();
        //controls.PlayerMain.Move.performed += context => Move(context.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        Debug.Log("The player wants to move: " + direction);
    }

    void WeMove()
    {
        //Debug.Log("We move the player");
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && !controls.PlayerMain.Jump.IsPressed()) _playerVelocity.y = 0f;
        else if (groundedPlayer && controls.PlayerMain.Jump.IsPressed()) _playerVelocity.y = 3f;
        controller.Move(jumpSpeed * Time.deltaTime * _playerVelocity.normalized);

        Vector2 movementInput = controls.PlayerMain.Move.ReadValue<Vector2>();
        float horizontal = movementInput.x;
        float vertical = movementInput.y;
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(speed * Time.deltaTime * moveDir.normalized);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(speed * Time.deltaTime * _playerVelocity.normalized);
    }

    void WeLook()
    {
        //Debug.Log("We change the look direction");
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Gravity()
    {
        _playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(jumpSpeed * Time.deltaTime * _playerVelocity.normalized);
    }

    // Update is called once per frame
    private void Update()
    {
        //Keyboard kb = InputSystem.GetDevice<Keyboard>();
        //if (kb.spaceKey.wasPressedThisFrame)
        //{
        //    Debug.Log("Someone pressed space?");
        //}
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        WeMove();
        Gravity();
        if (controls.PlayerMain.Pause.triggered)
        {
            PauseGame();
            //Debug.Log("Is triggered? " + controls.PlayerMain.Pause.triggered);
            //Debug.Log("To string: " + controls.PlayerMain.Pause.controls.ToString());
        }
    }

    private void PauseGame()
    {
        this.gameObject.SetActive(false);
        canvasObject.SetActive(true);
    }
}
