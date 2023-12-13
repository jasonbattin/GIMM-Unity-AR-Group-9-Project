
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static int highScore;
    public static int currentScore;
    public static string returnScene;

    //ends game
    public static void gameOver()
    {
        returnScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Game Over");
        Debug.Log(highScore);

    }

    //updates score
    public static void updateScore(int score)
    {
        currentScore += score;
        if (highScore < currentScore) { highScore = currentScore; }

    }

}
