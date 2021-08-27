using UnityEngine;

public class InputController : MonoBehaviour
{
    private InputSettings _inputSettings;

    private void Awake()
    {
        ChangeInputSettingToWAD();
    }

    private void Update()
    {
        _inputSettings.CheckInput();
    }

    private void ChangeInputSettings(InputSettings inputSettings)
    {
        _inputSettings = inputSettings;
    }

    public void ChangeInputSettingToMouse()
    {
        ChangeInputSettings(new InputControllerMouse(Camera.main));
    }

    public void ChangeInputSettingToWAD()
    {
        ChangeInputSettings(new InputControllerWAD());
    }

}
