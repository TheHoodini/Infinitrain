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

    }
    
    public void Spawn(){
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        transform.position = new Vector3(x, 1.5f, z);
    }
}
