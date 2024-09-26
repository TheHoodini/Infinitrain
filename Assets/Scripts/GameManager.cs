using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "SCORE: " + score.ToString();
        CheckScore5();
    }

    private void CheckScore5()
    {
        if (score % 5 == 0 && score > 0) 
        {
            AudioManager.instance.ScoreSound();
            SnakeController.instance.SpeedUp();
        }
    }

    public GameOverScreen gameOverScreen;
    public void GameOver()
    {
        scoreText.gameObject.SetActive(false);
        gameOverScreen.GameOver(score);
    }

}
