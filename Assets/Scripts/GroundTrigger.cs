using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GroundTrigger : MonoBehaviour
{
    public TrashCanCatcher catcher;
    public GameObject shopUI;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI strikeText;
    public GameObject gameOverUI;

    private int strikes = 0;
    private int maxStrikes = 3;

    private void Start()
    {
        UpdateStrikeText();
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FallingItem item = other.GetComponent<FallingItem>();
        if (item == null) return;

        if (item.itemType == FallingItem.ItemType.Trash)
        {
            // Add a strike
            strikes++;
            UpdateStrikeText();
            Debug.Log("Missed trash! Strikes: " + strikes);

            // Pause the game and open shop
            Time.timeScale = 0f;
            if (shopUI != null)
                shopUI.SetActive(true);

            if (strikes >= maxStrikes)
            {
                EndGame();
            }
        }
        else if (item.itemType == FallingItem.ItemType.Valuable)
        {
            // Reward for missing valuable
            catcher.money += 5;
            Debug.Log("Avoided Valuable! +5 money. Money: " + catcher.money);
        }

        Destroy(other.gameObject);
    }

    private void UpdateStrikeText()
    {
        if (strikeText != null)
        {
            strikeText.text = "Strikes: " + strikes + " / " + maxStrikes;
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (shopUI != null)
            shopUI.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RecoverStrike()
    {
        if (strikes > 0)
        {
            strikes--;
            UpdateStrikeText();
            Debug.Log("Strike recovered! Strikes: " + strikes);
        }
        else
        {
            Debug.Log("No strikes to recover!");
        }
    }
}