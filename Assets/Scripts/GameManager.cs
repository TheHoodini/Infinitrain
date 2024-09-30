using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public GameObject pwLever;
    public GameObject pwCarriage;
    public bool powerActivated = false;

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
        scoreText.text = $"SCORE: {score}";
        CheckScore5();
        
        if (score > 5 && !powerActivated && Random.value < 0.4f)
        {
            SpawnPowerUp();
        }
    }

    private void CheckScore5()
    {
        if (score % 5 == 0 && score > 0)
        {
            AudioManager.instance.ScoreSound();
            TrainController.instance.SpeedUp();
        }
    }

    public void SpawnPowerUp()
    {
        powerActivated = true;
        GameObject powerUp;
        if (Random.value < 0.5f)
        {
            powerUp = Instantiate(pwLever, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            powerUp = Instantiate(pwCarriage, new Vector3(0, 0, 0), Quaternion.identity);
        }
        StartCoroutine(DestroyPowerUpAfterTime(powerUp, 10f));
    }

    private IEnumerator DestroyPowerUpAfterTime(GameObject powerUp, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (powerUp != null)
        {
            Destroy(powerUp);
            powerActivated = false;
        }
    }

    public GameOverScreen gameOverScreen;
    public void GameOver()
    {
        scoreText.gameObject.SetActive(false);
        gameOverScreen.GameOver(score);
    }

}
