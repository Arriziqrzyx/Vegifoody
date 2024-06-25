using UnityEngine;
using System.Collections.Generic;

public class SayurTerbang : MonoBehaviour
{
    public float minY = 0.5f;
    public float maxY = 5f;
    public float minX = -9.5f;
    public float maxX = 9f;
    public float minDistance = 1f; 

    public float speed = 1f; 
    public float rotationSpeed = 30f; 

    private Rigidbody2D[] sayurRigidbodies;
    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        
        int childCount = transform.childCount;
        sayurRigidbodies = new Rigidbody2D[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = child.gameObject.AddComponent<Rigidbody2D>();
            }
            rb.gravityScale = 0; 
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
            sayurRigidbodies[i] = rb;

            
            if (child.GetComponent<Collider2D>() == null)
            {
                child.gameObject.AddComponent<CircleCollider2D>(); 
            }

            
            Vector3 randomPosition;
            do
            {
                float randomX = Random.Range(minX, maxX);
                float randomY = Random.Range(minY, maxY);
                randomPosition = new Vector3(randomX, randomY, child.position.z);
            } while (IsTooCloseToOthers(randomPosition));

            spawnedPositions.Add(randomPosition);
            child.position = randomPosition;

            
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            rb.velocity = randomDirection * speed;
        }
    }

    bool IsTooCloseToOthers(Vector3 position)
    {
        foreach (Vector3 pos in spawnedPositions)
        {
            if (Vector3.Distance(pos, position) < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        foreach (Rigidbody2D rb in sayurRigidbodies)
        {
            
            rb.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            
            Vector2 pos = rb.position;

            if (pos.x < minX || pos.x > maxX)
            {
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            }

            if (pos.y < minY || pos.y > maxY)
            {
                pos.y = Mathf.Clamp(pos.y, minY, maxY);
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
            }

            rb.position = pos;
        }
    }
}
