using UnityEngine;

public class TongueController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Snake":
                SnakeController.instance.Explode();
                break;
            case "Food":
                other.gameObject.GetComponent<FoodController>().Spawn();
                GameManager.instance.AddScore(1);
                SnakeController.instance.Grow();
                AudioManager.instance.EatSound();
                break;
            case "PwLever":
                SnakeController.instance.SpeedDown();
                AudioManager.instance.PowerUpSound();
                Destroy(other.gameObject);
                GameManager.instance.powerActivated = false;
                break;
            case "PwCarriage":
                SnakeController.instance.RemoveTailPart();
                AudioManager.instance.PowerUpSound();
                Destroy(other.gameObject);
                GameManager.instance.powerActivated = false;
                break;
        }
    }
}
