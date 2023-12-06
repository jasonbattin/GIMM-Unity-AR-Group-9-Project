using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Variables to time when the platform falls and when it is deleted
    [SerializeField] private float fallDelay = 1f; // Delay before the platform falls after a collision with the player
    [SerializeField] private float destroyDelay = 0.25f; // Delay before the platform is destroyed after falling

    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody2D component

    // This method is called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start a coroutine to make the platform fall
            StartCoroutine(Fall());
        }
    }

    // Coroutine to make the platform fall
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay); // Wait for the specified fall delay

        // Set the platform's Rigidbody2D to Dynamic, allowing it to fall
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = .9f;

        // Change the platform's layer to layer 3 (assuming layer 3 has been defined in your project)
        transform.gameObject.layer = 3;

        // Destroy the platform after a specified delay
        Destroy(gameObject, destroyDelay);
    }
}

