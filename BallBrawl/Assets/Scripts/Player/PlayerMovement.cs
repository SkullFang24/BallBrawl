using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private GameObject focalPoint;
    [SerializeField] private float speed;
    [SerializeField] private float maxVelocityMagnitude;

    private Vector2 movementInput;
    private NewControls controls;
    private bool _canMoveForward = true;
    private bool _canMoveBackward = true;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");

        if( Application.isMobilePlatform )
        {
            speed *= 2;
            maxVelocityMagnitude *= 2;
        }
    }

    private void Update()
    {
        Vector3 velocity = playerRb.velocity;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocityMagnitude);
        playerRb.velocity = velocity;

        if (_canMoveForward && movementInput.y > 0)
        {
            playerRb.AddForce(focalPoint.transform.forward * speed * movementInput.y);
        }
        else if (_canMoveBackward && movementInput.y < 0)
        {
            playerRb.AddForce(-focalPoint.transform.forward * speed * Mathf.Abs(movementInput.y));
        }

        _canMoveBackward = true;
        _canMoveForward = true;
    }

    private void OnEnable()
    {
        controls = new NewControls();
        controls.Player.Enable();
        controls.Player.Move.performed += ctx => {
            movementInput = ctx.ReadValue<Vector2>();
        };
        controls.Player.Move.canceled += ctx => {
            movementInput = Vector2.zero;
        };
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
