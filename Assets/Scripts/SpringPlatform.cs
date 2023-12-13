using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public float launchForce = 10f; // Adjust the force as needed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Make sure the player has the tag "Player"
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRigidbody != null)
            {
                // Apply an upward force to the player
                playerRigidbody.AddForce(new Vector2(0, launchForce), ForceMode2D.Impulse);
            }
        }
    }
}
