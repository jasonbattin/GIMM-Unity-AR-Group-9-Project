using UnityEngine;

public class CoinBobbleEffect : MonoBehaviour
{
    public float bobbleScale = 1.1f; // The scale to bobble to
    public float bobbleHeight = 0.5f; // The height to bobble to
    public float duration = 0.5f; // Duration of one bobble cycle

    private Vector3 originalScale; // To store the original scale of the coin
    private Vector3 originalPosition; // To store the original position of the coin

    void Start()
    {
        originalScale = transform.localScale; // Save the original scale
        originalPosition = transform.position; // Save the original position
        StartBobble();
    }

    void StartBobble()
    {
        // Scale animation
        LeanTween.scale(gameObject, new Vector3(bobbleScale, bobbleScale, bobbleScale), duration)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong();

        // Position animation (bobbing up and down)
        LeanTween.moveY(gameObject, originalPosition.y + bobbleHeight, duration)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong();
    }
}
