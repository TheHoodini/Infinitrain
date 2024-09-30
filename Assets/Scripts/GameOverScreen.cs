using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreTextEnd;
    int highScore;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void GameOver(int score)
    {   
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            highScore = score;
        }
        else
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }

        scoreTextEnd.text = "SCORE: " + score.ToString() + "\n" + "HIGH SCORE: " + highScore.ToString();
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainGame");
    }
}
