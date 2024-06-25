using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPointMove : MonoBehaviour
{
    [SerializeField] float speed; 
    [SerializeField] int startingPoint; 
    [SerializeField] Transform[] points; 

    private int currentIndexPoint; 

    private void Start()
    {
       
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        
        if (Vector2.Distance(transform.position, points[currentIndexPoint].position) < 0.1f)
        {
            currentIndexPoint++;
            if (currentIndexPoint == points.Length)
            {
                currentIndexPoint = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[currentIndexPoint].position, speed * Time.deltaTime);
    }
}
