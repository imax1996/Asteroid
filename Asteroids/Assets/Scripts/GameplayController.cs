using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private GameObject         _player = null;
    [SerializeField] private LevelController    _levelController = null;
    [SerializeField] private GameObject         _canvasMenu = null;
    [SerializeField] private GameObject         _continueButton = null;

    private void Start()
    {
        _player.GetComponent<Player>().GameOverEvent += OnGameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _canvasMenu.activeInHierarchy == false)
        {
            Time.timeScale = 0;
            _canvasMenu.SetActive(true);
            _continueButton.SetActive(true);
        }
    }

    private void OnGameOver()
    {
        _canvasMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        _continueButton.SetActive(false);
        _canvasMenu.SetActive(false);
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        _canvasMenu.SetActive(false);
        _player.GetComponent<Player>().Init();
        _levelController.StartNewGame();
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
