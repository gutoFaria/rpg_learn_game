using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    private PlayerControls controls;
    private PlayerController _controller;
    private GunController _gunController;

    // inputs controls controller
    private Vector2 moveInput;
    private Vector2 aimInput;

    // var for movement 
    [SerializeField] private float moveSpeed;
    
     // controle de aim
    [Header("Aim info")]
    //[SerializeField] private Transform aim;
    [SerializeField] private LayerMask aimLayerMask;
    private Vector3 lookingDirection;


    void Awake()
    {
        AssignInputEvents();
    }

    protected override void Start()
    {
        base.Start();
        _controller = GetComponent<PlayerController>();
        _gunController = GetComponent<GunController>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 moveVelocity = direction.normalized * moveSpeed;
        _controller.Move(moveVelocity);


        // Look input
        _controller.AimTowardsMouse(aimInput,aimLayerMask,lookingDirection);

        // Weapon input
        // if (Input.GetMouseButton(0))
        // {
        //     _gunController.Shoot();
        // }
    }

    #region NEW INPUT SYSTEM
    private void AssignInputEvents()
    {
        controls = new PlayerControls();

        controls.Character.Fire.performed += context => _gunController.Shoot();

        controls.Character.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Character.Movement.canceled += context => moveInput = Vector2.zero;

        controls.Character.Aim.performed += context => aimInput = context.ReadValue<Vector2>();
        controls.Character.Aim.canceled += context => aimInput = Vector2.zero;

    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
    #endregion
}
