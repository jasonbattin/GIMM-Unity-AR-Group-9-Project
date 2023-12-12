using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class HeartHealthBar : MonoBehaviour
{
    public GameObject heartContainer;
    public GameObject fullHeartPrefab;
    public GameObject emptyHeartPrefab;
    private List<GameObject> hearts = new List<GameObject>();
    public float fadeInDuration = 0.5f; // Duration of the fade-in effect
    public static HeartHealthBar Instance { get; private set; }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        heartContainer = GameObject.Find("HeartContainer");
        if (heartContainer == null)
        {
            Debug.LogError("HeartHealthBar: Failed to find HeartContainer in the scene.");
        }
    }


    public void InitializeHearts(int maxHealth)
    {
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(fullHeartPrefab, heartContainer.transform);
            Image heartImage = heart.GetComponent<Image>();
            heartImage.color = new Color(heartImage.color.r, heartImage.color.g, heartImage.color.b, 0);
            StartCoroutine(FadeInHeart(heartImage));
            hearts.Add(heart);
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].GetComponent<Image>().sprite = i < currentHealth ? fullHeartPrefab.GetComponent<Image>().sprite : emptyHeartPrefab.GetComponent<Image>().sprite;
        }
    }
    public void RefreshHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            Image heartImage = hearts[i].GetComponent<Image>();
            heartImage.sprite = i < currentHealth ? fullHeartPrefab.GetComponent<Image>().sprite : emptyHeartPrefab.GetComponent<Image>().sprite;

            // Apply fade-in effect if the heart is being refreshed to a full state
            if (i < currentHealth)
            {
                heartImage.color = new Color(heartImage.color.r, heartImage.color.g, heartImage.color.b, 0);
                StartCoroutine(FadeInHeart(heartImage));
            }
        }
    }

    public void AddHeart()
    {
        if (emptyHeartPrefab == null)
        {
            Debug.LogError("HeartHealthBar: emptyHeartPrefab is null.");
            return;
        }
        if (heartContainer == null)
        {
            Debug.LogError("HeartHealthBar: heartContainer is null.");
            return;
        }
        GameObject heart = Instantiate(emptyHeartPrefab, heartContainer.transform);
        Image heartImage = heart.GetComponent<Image>();
        heartImage.color = new Color(heartImage.color.r, heartImage.color.g, heartImage.color.b, 0);
        StartCoroutine(FadeInHeart(heartImage));
        hearts.Add(heart);
    }

    private IEnumerator FadeInHeart(Image heartImage)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            heartImage.color = new Color(heartImage.color.r, heartImage.color.g, heartImage.color.b, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        heartImage.color = new Color(heartImage.color.r, heartImage.color.g, heartImage.color.b, 1);
    }
}