
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject particleEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (particleEffectPrefab != null)
            {
                Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            Score.updateScore(1);
        }
    }
}

