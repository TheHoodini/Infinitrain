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
                TrainController.instance.Explode();
                break;
            case "Food":
                other.gameObject.GetComponent<FoodController>().Spawn();
                GameManager.instance.AddScore(1);
                TrainController.instance.Grow();
                AudioManager.instance.EatSound();
                break;
            case "PwLever":
                TrainController.instance.SpeedDown();
                AudioManager.instance.PowerUpSound();
                Destroy(other.gameObject);
                GameManager.instance.powerActivated = false;
                break;
            case "PwCarriage":
                TrainController.instance.RemoveTailParts();
                AudioManager.instance.PowerUpSound();
                Destroy(other.gameObject);
                GameManager.instance.powerActivated = false;
                break;
        }
    }
}
