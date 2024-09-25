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
        if (other.gameObject.CompareTag("Snake"))
        {
            SnakeController.instance.Explode();
        }
        else if (other.gameObject.CompareTag("Food"))
        {
            other.gameObject.GetComponent<FoodController>().Spawn();
            GameManager.instance.AddScore(1);
            SnakeController.instance.Grow();
            AudioManager.instance.EatSound();
        }
    }
}
