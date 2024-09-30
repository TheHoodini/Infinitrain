
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private bool isMoving = true;
    public float MoveSpeed = 5;
    public float RotationSpeed = 180;
    public float BodySpeed = 15;
    public int TailGap = 150;
    public int ExplosionForce = 600;

    public Transform tongue;
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
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            MoveSpeed *= 0.9f;
            BodySpeed *= 0.9f;
            TailGap = Mathf.RoundToInt(TailGap * 0.75f); // Adjust as needed
        }
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
            int cont = 1;
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

    public void SpeedUp()
    {
        MoveSpeed += 1f;
        BodySpeed += 1f;
    }

    public void Explode()
    {
        AudioManager.instance.GameOverSound();
        GameManager.instance.GameOver();
        tongue.gameObject.SetActive(false);
        
        Vector3 snakePosition = transform.position;
        isMoving = false;

        // Add explosion force to the snake head
        Rigidbody snakeRb = GetComponent<Rigidbody>();
        snakeRb.isKinematic = false;
        snakeRb.AddExplosionForce(ExplosionForce, snakePosition, 10);

        foreach (var body in Tail)
        {
            // Check if the body part exists to prevent null reference exceptions
            if (body != null)
            {
                Rigidbody rb = body.GetComponent<Rigidbody>();
                if (rb == null) rb = body.AddComponent<Rigidbody>();
                rb.AddExplosionForce(ExplosionForce, snakePosition, 10);
            }
        }
    }



    public void Grow()
    {
        Vector3 spawnPosition = Tail.Count > 0 ? Tail[Tail.Count - 1].transform.position : transform.position;

        // Instantiate new tail part at the spawn position
        GameObject body = Instantiate(TailPrefab, spawnPosition, Quaternion.identity);
        Tail.Add(body);

        if (Tail.Count > 1) body.transform.rotation = Tail[Tail.Count - 2].transform.rotation;

    }


}
