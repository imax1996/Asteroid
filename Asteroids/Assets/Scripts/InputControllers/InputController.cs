using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.InputControllers
{
    public class InputController : MonoBehaviour
    {
        private InputSettings _inputSettings;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private TextMeshProUGUI _text;

        private void Awake()
        {
            ChangeInputSettingToMouse();
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
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonSettings.onClick.AddListener(ChangeInputSettingToWAD);
            _text.text = " À¿¬»¿“”–¿ + Ã€ÿ‹";
        }

        public void ChangeInputSettingToWAD()
        {
            ChangeInputSettings(new InputControllerWAD());
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonSettings.onClick.AddListener(ChangeInputSettingToMouse);
            _text.text = " À¿¬»¿“”–¿";
        }

    }
}