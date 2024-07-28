using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{

    private void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerController.instance.Jump();
            }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {

        PlayerController.instance.Jump();
        Debug.Log("Working?");
    }
}
