using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float xBound = 8f;

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + Vector3.right * moveInput * moveSpeed * Time.deltaTime;

        // Clamp the position so the player doesn't go off screen
        newPosition.x = Mathf.Clamp(newPosition.x, -xBound, xBound);
        transform.position = newPosition;
    }
}
