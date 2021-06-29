using UnityEngine;

public class Balloon : MonoBehaviour
{
    #region --Fields / Properties--
    
    /// <summary>
    /// Left/right force back on wind direction keys.
    /// </summary>
    [SerializeField]
    private float _windForceAmount;

    /// <summary>
    /// Key when pressed applies a wind force from the left of the screen.
    /// </summary>
    [SerializeField]
    private KeyCode _windFromLeftKey;
    
    /// <summary>
    /// Key when pressed applies a wind force from the right of the screen.
    /// </summary>
    [SerializeField]
    private KeyCode _windFromRightKey;
    
    /// <summary>
    /// Constant downward force amount.
    /// </summary>
    [SerializeField]
    private float _gravityForceAmount;
    
    /// <summary>
    /// Constant upward force amount.
    /// </summary>
    [SerializeField]
    private float _heliumForceAmount;
    
    /// <summary>
    /// Speed and direction of the balloon.
    /// </summary>
    private Vector3 _velocity;

    /// <summary>
    /// Change in velocity of the balloon.
    /// </summary>
    private Vector3 _acceleration;

    /// <summary>
    /// Provides the force vector for wind.
    /// </summary>
    private Vector3 _windForce;
    
    /// <summary>
    /// Provides the force vector for gravity.
    /// </summary>
    private Vector3 _gravityForce;

    /// <summary>
    /// Provides the force vector for the helium in the balloon.
    /// </summary>
    private Vector3 _heliumForce;

    /// <summary>
    /// Cached Transform component.
    /// </summary>
    private Transform _transform;
    
    #endregion
    
    #region --Unity Specific Methods--

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        ApplyForce(_gravityForce);
        ApplyForce(_heliumForce);
        Move();
    }

    private void OnCollisionEnter(Collision _other)
    {
        _velocity = Vector3.Reflect(_velocity, _other.contacts[0].normal);
        _velocity *= .4f;
    }

    private void OnCollisionStay(Collision other)
    {
        _velocity.y *= .2f;
        _velocity.x *= .95f;
    }
    
    #endregion
    
    #region --Custom Methods--

    /// <summary>
    /// Initializes variables and caches components.
    /// </summary>
    private void Init()
    {
        _windForce = new Vector3(_windForceAmount, 0, 0);
        _gravityForce = new Vector3(0, -_gravityForceAmount, 0);
        _heliumForce = new Vector3(0, _heliumForceAmount, 0);
        _transform = transform;
    }

    /// <summary>
    /// Check for input from the wind direction keys and apply wind.
    /// </summary>
    private void CheckInput()
    {
        if (Input.GetKey(_windFromLeftKey))
        {
            ApplyForce(-_windForce);
        }

        if (Input.GetKey(_windFromRightKey))
        {
            ApplyForce(_windForce);
        }
    }

    /// <summary>
    /// Adds the _force provided to acceleration to create a net force effect.
    /// </summary>
    /// <param name="_force"></param>
    private void ApplyForce(Vector3 _force)
    {
        _acceleration += _force;
    }

    /// <summary>
    /// Handles movement of the balloon.
    /// </summary>
    private void Move()
    {
        _velocity += _acceleration;
        _transform.position += _velocity * Time.deltaTime;

        _acceleration = Vector3.zero;
    }
    
    #endregion
    
}
