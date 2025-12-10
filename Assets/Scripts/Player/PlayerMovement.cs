using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //private Camera mainCamera;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private Vector2 velocity; // eigene Geschwindigkeitsberechnung
    private Vector2 inputAxis; // wie stark der Spieler auf die Tasten drückt
    
    public AudioClip m_jumpSound;

    // arrow syntax: turning it into a property, computing this based on other values
    private float jumpForce; // half the time -> multiply and dividing by time
    private float gravity;

    // properties that have a public getter, but a private setter -> need to read the value in other scripts
    public bool grounded { get; private set; } 
    public bool jumping { get; private set; }
    //public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    //public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); // turning movement 

    //public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_jumpAction;
    
    private Vector2 m_moveValue;

    private PlayerStats m_playerStats;
    
    private void Awake()
    {
        InputActionAsset inputActions = InputSystem.actions; 
        
        m_moveAction = inputActions.FindAction("Move");
        m_jumpAction = inputActions.FindAction("Jump");
        
        rb = GetComponent<Rigidbody2D>();

        m_playerStats = this.gameObject.GetComponent<Player>().m_playerStats;

        jumpForce = (2f * m_playerStats.MaxJumpHeight) / (m_playerStats.MaxJumpTime / 2f);
        gravity = (-2f * m_playerStats.MaxJumpHeight) / Mathf.Pow((m_playerStats.MaxJumpTime / 2f), 2);

        //mainCamera = Camera.main;
        //audioSource = GetComponent<AudioSource>();
    }
    
    private void OnEnable()
    {
        m_moveAction.Enable();
        m_jumpAction.Enable();
    }

    private void OnDisable()
    {
        m_moveAction.Disable();
        m_jumpAction.Disable();
    }

    private void Update()
    {
        HorizontalMovement();
        
        grounded = CheckGrounded();

        if (grounded)
            GroundedMovement();

        ApplyGravity();
    }

    private void HorizontalMovement()
    {
        inputAxis = m_moveAction.ReadValue<Vector2>();
        
        velocity.x = Mathf.MoveTowards(
            velocity.x,
            inputAxis.x * m_playerStats.MovementSpeed,
            m_playerStats.MovementSpeed * Time.deltaTime * 3f
        );

 
        
        /*if (CheckWall(velocity.x))
        {
            velocity.x = 0f;
        }*/

        //Debug.Log(velocity.x);
        
        // Sprite flip
        if (velocity.x > 0)
            transform.eulerAngles = Vector3.zero;
        else if (velocity.x < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (m_jumpAction.WasPressedThisFrame())
        {
            velocity.y = jumpForce;
            jumping = true;

            SFXManager.Instance.PlaySFXClip(m_jumpSound, this.gameObject.transform, 1f);
            //if (jumpSound && audioSource)
                //audioSource.PlayOneShot(jumpSound);
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f;
        float multiplier = falling ? 5f : 2f;

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 pos = rb.position;
        pos += velocity * Time.fixedDeltaTime;

        //Debug.Log("Pos: " + pos);

        /*Vector2 left = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 right = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        pos.x = Mathf.Clamp(pos.x, left.x + 0.5f, right.x - 0.5f);*/

        rb.MovePosition(pos);
    }

    private bool CheckGrounded()
    {
        Vector2 origin = rb.position + Vector2.down * 0.1f;
        return Physics2D.Raycast(origin, Vector2.down, 0.2f, ~0);
    }

    private bool CheckWall(float direction)
    {
        Vector2 origin = rb.position;
        Vector2 dir = new Vector2(Mathf.Sign(direction), 0);

        return Physics2D.Raycast(origin, dir, 0.2f, ~0);
    }
}