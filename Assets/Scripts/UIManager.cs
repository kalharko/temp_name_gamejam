using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.IsGameOver)
        {
            scoreText.text = "SCORE : " + GameManager.Instance.Score;
            gameOverScreen.SetActive(true);
        }
        else
        {
            gameOverScreen.SetActive(false);
        }
    }
}
