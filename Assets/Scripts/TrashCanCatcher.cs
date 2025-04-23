using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TrashCanCatcher : MonoBehaviour
{
    public int money = 0;
    private int trashCollected = 0;

    public AudioSource audioSource;       
    public AudioClip trashSound;          
    public AudioClip valuableSound;
    public TextMeshProUGUI moneyText;
    public TrashCanCatcher catcher;
    public TrashManager trashManager;
    void UpdateMoneyText()
    {
        moneyText.text = $"Money: ${catcher.money}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FallingItem item = other.GetComponent<FallingItem>();
        if (item != null)
        {
            if (item.itemType == FallingItem.ItemType.Trash)
            {
                money += 10;
                trashCollected++;
                if (trashCollected % 10 == 0 && trashManager != null)
                {
                    trashManager.DecreaseSpawnInterval(0.2f);
                }

                if (audioSource != null && trashSound != null)
                {
                    audioSource.PlayOneShot(trashSound);
                }

                Debug.Log("Caught Trash! Money: " + money);
                UpdateMoneyText();
            }
            else if (item.itemType == FallingItem.ItemType.Valuable)
            {
                money -= 5;

                if (audioSource != null && valuableSound != null)
                {
                    audioSource.PlayOneShot(valuableSound);
                }

                Debug.Log("Caught Valuable! Money: " + money);
                UpdateMoneyText();
            }

            Destroy(other.gameObject);
        }
    }
}

