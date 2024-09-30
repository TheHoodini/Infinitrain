
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    private bool isMoving = true;
    public float moveSpeed = 5;
    public float rotationSpeed = 300;
    //public float bodySpeed = 15;
    public int tailGap = 15;
    public int explosionForce = 600;

    public Transform tongue;
    public GameObject TailPrefab;
    private List<GameObject> Tail = new List<GameObject>();
    private List<Vector3> TailPositions = new List<Vector3>();

    public static TrainController instance;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // Move forward
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            // Left and right rotation
            float direction = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime);

            // Save tail parts positions
            TailPositions.Insert(0, transform.position);

            // Move tail parts
            int cont = 1;
            foreach (var body in Tail)
            {
                Vector3 temp = TailPositions[Mathf.Min(cont * tailGap, TailPositions.Count - 1)];
                Vector3 moveDirection = temp - body.transform.position;
                body.transform.position += moveDirection * moveSpeed * Time.deltaTime;
                body.transform.LookAt(temp);
                cont++;
            }
        }

    }

    public void SpeedUp()
    {
        moveSpeed += 1f;
    }

    public void SpeedDown()
    {
        if (moveSpeed > 1)
        {
            moveSpeed -= 0.5f;
        }
    }

    public void Explode()
    {
        AudioManager.instance.GameOverSound();
        GameManager.instance.GameOver();
        tongue.gameObject.SetActive(false);

        Vector3 trainPosition = transform.position;
        isMoving = false;

        // Add explosion force to the train head
        Rigidbody trainRb = GetComponent<Rigidbody>();
        trainRb.isKinematic = false;
        trainRb.AddExplosionForce(explosionForce, trainPosition, 10);

        foreach (var body in Tail)
        {
            if (body != null)
            {
                // Add explosion force to the tail parts
                Rigidbody rb = body.GetComponent<Rigidbody>();
                if (rb == null) rb = body.AddComponent<Rigidbody>();
                rb.AddExplosionForce(explosionForce, trainPosition, 10);
            }
        }
    }

    public void Grow()
    {
        Vector3 spawnPosition = Tail.Count > 0 ? Tail[Tail.Count - 1].transform.position : transform.position;

        GameObject body = Instantiate(TailPrefab, spawnPosition, Quaternion.identity);
        Tail.Add(body);

        if (Tail.Count > 1) body.transform.rotation = Tail[Tail.Count - 2].transform.rotation;

    }

    public void RemoveTailParts()
    {
        // Remove 5% of the tail parts
        int partsToRemove = Mathf.CeilToInt(Tail.Count * 0.05f);
        for (int i = 0; i < partsToRemove; i++)
        {
            if (Tail.Count > 0)
            {
                GameObject tailPart = Tail[Tail.Count - 1];
                Tail.RemoveAt(Tail.Count - 1);
                Destroy(tailPart);
            }
        }
    }


}
