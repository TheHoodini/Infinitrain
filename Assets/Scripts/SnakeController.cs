
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private bool isMoving = true;
    public float MoveSpeed = 5;
    public float RotationSpeed = 180;
    public int BodySpeed = 5;
    public int TailGap = 150;
    public int ExplosionForce = 600;

    public GameObject TailPrefab;
    private List<GameObject> Tail = new List<GameObject>();
    private List<Vector3> TailPositions = new List<Vector3>();

    public static SnakeController instance;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
       Grow(); 
    }

    void Update()
    {
        if (isMoving)
        {
            // Move forward
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            // Left and right rotation
            float direction = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, direction * RotationSpeed * Time.deltaTime);

            // Save tail parts positions
            TailPositions.Insert(0, transform.position);

            // Move tail parts
            int cont = 0;
            foreach (var body in Tail)
            {
                Vector3 temp = TailPositions[Mathf.Min(cont * TailGap, TailPositions.Count - 1)];
                Vector3 moveDirection = temp - body.transform.position;
                body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
                body.transform.LookAt(temp);
                cont++;
            }
        }

    }

    public void Explode()
    {
        Vector3 snakePosition = transform.position;
        isMoving = false;

        foreach (var body in Tail)
        {
            // Check if the body part exists to preent null reference exceptions
            if (body != null) 
            {
                Rigidbody rb = body.GetComponent<Rigidbody>();
                if (rb == null) rb = body.AddComponent<Rigidbody>();
                rb.AddExplosionForce(ExplosionForce, snakePosition, 5); 
            }
            else
            {
                Debug.LogWarning("A tail part is null in the Tail list");
            }
        }

        // Destroy(gameObject, 3f); 
    }



    public void Grow()
    {
        GameObject body = Instantiate(TailPrefab);
        Tail.Add(body);
    }

}
