
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<Health>().takeDamage(damage);
            collision.gameObject.GetComponent<TimeStop>().StopTime(0.0f, 10, 0.1f);
        }
    }
}
