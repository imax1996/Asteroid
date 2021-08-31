using UnityEngine;
using UnityEngine.Events;

public static class PlayerEvents
{
    public static event UnityAction MoveEvent;
    public static event UnityAction<float> RotateEvent;
    public static event UnityAction<Vector3> RotateMouseEvent;
    public static event UnityAction ShotEvent;

    public static void OnMove()
    {
        MoveEvent?.Invoke();
    }

    public static void OnRotate(float plusOnZAxis)
    {
        RotateEvent?.Invoke(plusOnZAxis);
    }

    public static void OnMouseRotate(Vector3 mousePosition)
    {
        RotateMouseEvent?.Invoke(mousePosition);
    }

    public static void OnShot()
    {
        ShotEvent?.Invoke();
    }
}
