using UnityEngine;

public class InputControllerMouse : InputSettings
{
    private Camera _mainCamera;

    public InputControllerMouse(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }

    public override void CheckInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(1))
        {
            PlayerEvents.OnMove();
        }

        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        PlayerEvents.OnMouseRotate(mousePos);
    }
}
