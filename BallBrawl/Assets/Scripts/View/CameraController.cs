using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject FocalPoint;
    public float rotationSpeed;
    public bool isInverted;
    private float LastMouseX;
    private float CurrentMouseX;
    private float delta;

    private Vector2 movementInput;
    private NewControls controls;

    public static CameraController instance;

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
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


    private void Update()
    {

        if (Application.isEditor)
        {
            var horizontalAxis = movementInput.x;
            if (isInverted)
            {
                FocalPoint.transform.Rotate(Vector3.up, -horizontalAxis * rotationSpeed * 20 * Time.deltaTime);
            }
            else
            {
                FocalPoint.transform.Rotate(Vector3.up, horizontalAxis * rotationSpeed * 20 * Time.deltaTime);
            }

        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        LastMouseX = pointerEventData.position.x;

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        CurrentMouseX = pointerEventData.position.x;
        delta = CurrentMouseX - LastMouseX;
        LastMouseX = CurrentMouseX;

        if (isInverted)
        {
            FocalPoint.transform.Rotate(Vector3.up, -delta * rotationSpeed * Time.deltaTime);
        }
        else
        {
            FocalPoint.transform.Rotate(Vector3.up, delta * rotationSpeed * Time.deltaTime);
        }

    }
}
