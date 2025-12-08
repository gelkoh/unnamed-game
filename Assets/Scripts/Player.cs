using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    private Transform m_playerTransform;
    
    public InputActionAsset InputActions;

    private InputAction m_moveAction;
    private InputAction m_jumpAction;

    private Vector2 m_moveValue;
    
    [SerializeField]
    private float m_movementSpeed;

    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private int m_jumpForce;

	[SerializeField]
	private GameObject m_checkpoint;

    void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        
        m_playerTransform = this.transform;
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_jumpAction = InputSystem.actions.FindAction("Jump");
        
        m_rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //SceneManager.MoveGameObjectToScene(Player.Instance.gameObject, SceneManager.GetSceneByName("Main"));
    }

    void Update()
    {
        m_moveValue = m_moveAction.ReadValue<Vector2>();

        if (m_jumpAction.triggered)
        {
            //Debug.Log("JUMPED@!!");
            m_rigidbody.AddForce(Vector2.up * m_jumpForce);
        }

        // Ignore up/down movement
        m_moveValue.y = 0;
        
        //Debug.Log(m_moveValue);
        //Debug.Log(m_playerTransform);

        //Vector3 newPosition = new Vector3(m_playerTransform.localPosition.x += m_moveValue.x, m_playerTransform.localPosition.y, m_playerTransform.localPosition.z);

        //Vector3 positionChange = new Vector3(m_moveValue.x * m_movementSpeed * Time.deltaTime, 
                                             //m_moveValue.y * m_movementSpeed * Time.deltaTime, 
                                             //0);
        
        //m_playerTransform.localPosition = transform.localPosition + positionChange;

        m_rigidbody.AddForce(m_moveValue * m_movementSpeed);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }

    public void SetPosition(float x, float y, float z)
    {
        this.gameObject.transform.position = new Vector3(x, y, z);
    }
	
	public void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collided with " + other.tag);

		if (other.tag == "Water")
		{
			Die();
		}
	}

	private void Die()
	{
		Debug.Log("Player died");
		this.transform.localPosition = m_checkpoint.transform.localPosition;
	}
}