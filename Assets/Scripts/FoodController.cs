using UnityEngine;

public class FoodController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);

        // Move the object up and down
        float newY = Mathf.Sin(Time.time) * 0.2f + 1.8f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    public void Spawn(){
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        transform.position = new Vector3(x, 1.5f, z);
    }
}
