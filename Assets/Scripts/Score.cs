
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static int highScore;
    public static int currentScore;
    
    //ends game
    public static void gameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    //reset current Score
    public static void ResetScore()
    {
        currentScore = 0;
    }

    //if current score is high score, set high score as current score
    private void Update()
    {
        if (highScore > currentScore) {  highScore = currentScore; }
    }
}
