using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text currScoreText;
    [SerializeField] private TMP_Text highScoreText;

    public void respawn()
    {
        SceneManager.LoadScene(Score.returnScene);
    }

    private void Start()
    {
        currScoreText.text = "Score: " + Score.currentScore;
        highScoreText.text = "High Score: " + Score.highScore;
        Score.currentScore = 0;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
