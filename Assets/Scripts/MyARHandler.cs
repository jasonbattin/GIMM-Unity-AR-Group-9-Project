using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyARHandler : MonoBehaviour
{
    public Button playButton; // Assign in Inspector
    public string sceneToLoad; // Scene name to load, set this per image target in the Inspector

    void Start()
    {
        if (playButton != null)
        {
            playButton.gameObject.SetActive(false);
            playButton.onClick.AddListener(OnPlayButtonClicked); // Add click listener for the play button
        }
    }

    public void ActivateHaptics() // Called from the ImageTargetBehaviourScript
    {
        Handheld.Vibrate();
    }

    public void ShowPlayButton(bool show)
    {
        if (playButton != null)
        {
            playButton.gameObject.SetActive(show);
        }
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad); // Load the assigned scene
    }
}
