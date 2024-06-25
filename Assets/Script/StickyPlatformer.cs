using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatformer : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
        else
        {
            collision.transform.SetParent(null);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
    }

}