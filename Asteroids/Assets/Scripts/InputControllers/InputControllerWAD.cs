using UnityEngine;

namespace Asteroids.InputControllers
{
    public class InputControllerWAD : InputSettings
    {
        public override void CheckInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                PlayerEvents.OnMove();
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerEvents.OnRotate(1f);
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                PlayerEvents.OnRotate(-1f);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerEvents.OnShot();
            }
        }
    }
}