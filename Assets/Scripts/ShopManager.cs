using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;

    public GameObject shopButton;

    public PlayerController2D player;
    public TrashCanCatcher catcher;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI warningText;

    public GroundTrigger groundTrigger;
    public int strikeRecoveryCost = 100;

    private int speedCost = 5;
   


    void OnEnable()
    {
        UpdateMoneyText();
    }

    public void IncreaseSpeed()
    {
        AttemptPurchase(() => player.moveSpeed += 1f);
    }

    public void DecreaseSpeed()
    {
        AttemptPurchase(() => player.moveSpeed = Mathf.Max(1f, player.moveSpeed - 1f));
    }


    public void RecoverStrike()
    {
        if (catcher.money >= strikeRecoveryCost)
        {
            if (groundTrigger != null)
            {
                catcher.money -= strikeRecoveryCost;
                groundTrigger.RecoverStrike();
                warningText.gameObject.SetActive(false);
                UpdateMoneyText();
            }
            else
            {
                Debug.LogWarning("GroundTrigger is not assigned to ShopManager!");
            }
        }
        else
        {
            warningText.text = "Not enough money to recover a strike!";
            warningText.gameObject.SetActive(true);
        }
    }
    void AttemptPurchase(System.Action onSuccess)
    {
        if (catcher.money >= speedCost)
        {
            catcher.money -= speedCost;
            warningText.gameObject.SetActive(false);
            onSuccess.Invoke();
            UpdateMoneyText();
        }
        else
        {
            warningText.text = "Not enough money!";
            warningText.gameObject.SetActive(true);
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OpenShop()
    {
        Debug.Log("Shop opened");

        if (shopUI == null)
        {
            Debug.LogError("Shop UI is not assigned!");
            return;
        }

        shopUI.SetActive(true);
        Debug.Log("Shop UI should be visible now");

        Time.timeScale = 0f;
        Debug.Log("Game paused");

        if (shopButton != null)
            shopButton.SetActive(false);

        UpdateMoneyText();
    }

    void UpdateMoneyText()
    {
        moneyText.text = $"Money: ${catcher.money}";
    }
    public void ContinueGame()
    {
        if (shopUI != null)
            shopUI.SetActive(false);

        if (shopButton != null)
            shopButton.SetActive(true);

        if (warningText != null)
            warningText.gameObject.SetActive(false);


        Time.timeScale = 1f;
        Debug.Log("Game continued");
    }
}
