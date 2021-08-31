using UnityEngine;
using TMPro;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Player          _player;

    private void Start()
    {
        _player.HealthUIEvent += HealthUI;
        _player.ScoreUIEvent += ScoreUI;
    }

    private void HealthUI(int health)
    {
        _healthText.text = $"ÆÈÇÍÈ: {health}";
    }

    private void ScoreUI(int score)
    {
        _scoreText.text = $"Î×ÊÈ: {score}";
    }
}
