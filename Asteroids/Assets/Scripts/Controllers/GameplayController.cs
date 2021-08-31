using UnityEngine;
using UnityEngine.Audio;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private LevelController    _levelController;
    [SerializeField] private GameObject         _player;
    [SerializeField] private GameObject         _canvasMenu;
    [SerializeField] private GameObject         _continueButton;
    [SerializeField] private AudioMixer         _mixer;

    private void Start()
    {
        _player.GetComponent<Player>().GameOverEvent += OnGameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _canvasMenu.activeInHierarchy == false)
        {
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        _canvasMenu.SetActive(true);
        _continueButton.SetActive(true);
        _mixer.SetFloat("MasterVolume", -80f);
    }

    private void OnGameOver()
    {
        _continueButton.SetActive(false);
        _canvasMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        _continueButton.SetActive(false);
        _canvasMenu.SetActive(false);
        _mixer.SetFloat("MasterVolume", 0f);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        _canvasMenu.SetActive(false);
        _player.GetComponent<Player>().Init();
        _levelController.StartNewGame();
        _mixer.SetFloat("MasterVolume", 0f);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
