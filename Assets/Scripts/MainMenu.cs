
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the inspector with your VideoPlayer
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        highScoreText.text = "High Score: " + Score.highScore;
    }

    public void PlayGame(int scene)
    {
        LoadScene(scene);

    }

    void LoadScene(int scene)
    {
        //todo add logic deciding which scene to load based off AR image target
        SceneManager.LoadScene(scene);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading main menu...");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
