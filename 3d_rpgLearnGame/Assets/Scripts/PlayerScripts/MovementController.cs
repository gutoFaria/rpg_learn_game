using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private PlayerControls _controls;

    public float movementSpeed = 3.0f;
    private Vector2 movement;
    private Vector3 direction;
    private Rigidbody rb;

    // animations
    private Animator animator;
    string animationState = "AnimationState";

    // rotation
    public float rotationSpeed;
    Quaternion rot = Quaternion.identity;


    enum CharStates
    {
        walk = 1,
        idle = 2
    }

    void Awake()
    {
        _controls = new PlayerControls();

        _controls.Character.Movement.performed += context => movement = context.ReadValue<Vector2>();
        _controls.Character.Movement.canceled += context => movement = Vector2.zero;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateState();
    }

    void FixedUpdate()
    {
        MoveCharacter();
        RotateCharacter();
    }

    private void MoveCharacter()
    {
        direction = new Vector3(movement.x, 0f, movement.y);
        direction.Normalize();

        rb.linearVelocity = direction * movementSpeed;

    }

    private void RotateCharacter()
    {
        // if(direction != Vector3.zero)
        // {
        //     Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
        //     transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed);
        // }

        Vector3 desFor = Vector3.RotateTowards(transform.forward, movement, rotationSpeed, 0f);
        rot = Quaternion.LookRotation(desFor);
    }

    private void UpdateState()
    {
        
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }
}
