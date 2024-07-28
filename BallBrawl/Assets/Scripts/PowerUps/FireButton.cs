using UnityEngine;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler
{

    private void Update()
    {
        if( Application.isEditor )
        {
            if( Input.GetKeyDown(KeyCode.F))
            {
                PlayerController.instance.ShootProjectile();
            }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        PlayerController.instance.ShootProjectile();
            
    }
}
