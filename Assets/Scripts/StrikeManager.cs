using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StrikeManager : MonoBehaviour
{
    public int maxStrikes = 3;
    public int currentStrikes = 0;
    public GameObject gameOverUI; // Assign Game Over Panel
    public Text strikeText;       // Assign Strike UI text

    void Start()
    {
        UpdateStrikeUI();
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    public void AddStrike()
    {
        currentStrikes++;
        Debug.Log("Strike! Total: " + currentStrikes);
        UpdateStrikeUI();

        if (currentStrikes >= maxStrikes)
        {
            GameOver();
        }
    }

    void UpdateStrikeUI()
    {
        if (strikeText != null)
            strikeText.text = "Strikes: " + currentStrikes + " / " + maxStrikes;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
