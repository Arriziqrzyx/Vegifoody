using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(HandleCollision());
        }
    }

    private IEnumerator HandleCollision()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(1.5f);
        transform.position = startPosition;
        spriteRenderer.enabled = true;
    }
}
