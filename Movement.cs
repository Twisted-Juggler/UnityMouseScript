using UnityEngine;

public class Movement : MonoBehaviour
{
    [Space(5)]
    [Header("Developer tools")]
    
    // Dev only controlls
    
    public bool iGoUpNow = false; // flight
    public bool vroomVroom = false; // speed


    [Space(5)]
    [Header("Controllers")]
    
    // Controllers
   
    public Transform cam;
    public Transform player;
    public Rigidbody rb;
    public Transform orientation;
    public float gravityPull = 0f;
    
    [Space(5)]
    [Header("Player Variables")]
    
    // Player Variables
    
    public float playerSpeed = 1f;
    public float jumpHeight = 2;
    public float playerHeight = 4f;
    public bool isSprinting;
    

    [Space(5)]
    [Header("Movement Variables")]
   
    // Movement Variables
    
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float groundMultiplier = 2f;
    [SerializeField] private float airDrag = 2f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float speedMultiplier = 10f;
    private float x, y;
    private bool isGrounded;
    private float horizontalMovement;
    private float verticalMovement;
    

    KeyCode jumpKey = KeyCode.Space;
    
    Vector3 MoveDirection;
    Vector3 playerDown;

    // On Start
    void Start()
    { 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    // Every Frame
    void Update()
    {
        Gravity();
        MoveInput();
        ControlDrag();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .05f + 0.1f);

        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
    }


    /*--- Movement ---*/
    void MoveInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if(vroomVroom == true)
        {
            playerSpeed = 100;
        }

        else { }

        if(iGoUpNow == true)
        {
            rb.useGravity = false;
            gravityPull = 0f;
            MoveDirection = cam.forward * verticalMovement + cam.right * horizontalMovement;
        }
        
        else { }
    }

    void Move()
    {
        if (isGrounded)
        {
            rb.AddForce(MoveDirection.normalized * speedMultiplier * playerSpeed, ForceMode.Acceleration);
        }

        else
        {
            rb.AddForce(MoveDirection.normalized * speedMultiplier * playerSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }


    void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }



    /*--- Physics ---*/
    void Gravity()
    {
        rb.AddForce(Vector3.down * gravityPull, ForceMode.Acceleration);
    }

    void ControlDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag * groundMultiplier;
        }

        else
        {
            rb.drag = airDrag;
        }
    }
}
