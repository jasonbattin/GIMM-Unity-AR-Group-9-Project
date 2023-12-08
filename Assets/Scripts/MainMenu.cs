
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Include the video namespace

public class MainMenu : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign this in the inspector with your VideoPlayer


    public void PlayGame(int scene)
    {
        LoadScene(scene);
        /*Debug.Log("PlayGame method called");
        videoPlayer.loopPointReached += EndReached; // Subscribe to the event
        videoPlayer.Play();*/
    }

    /*private void EndReached(VideoPlayer vp)
    {
        Debug.Log("Video finished playing.");
        vp.loopPointReached -= EndReached; // Unsubscribe to avoid this method being called again
        LoadNextScene();
    }*/
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
    //logic for replaying level
    /*public void ReplayLevel()
    {
        SceneManager.LoadScene("example Level 1");
    }*/

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
