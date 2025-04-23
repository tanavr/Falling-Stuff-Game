using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public enum ItemType { Trash, Valuable }
    public ItemType itemType;

    public float fallSpeed = 3f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}