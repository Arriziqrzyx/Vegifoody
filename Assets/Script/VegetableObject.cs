using UnityEngine;
using TMPro;

public class VegetableObject : MonoBehaviour
{
    public bool isVegetable = true; 
    public int vegetablePoints = 10; 
    public int nonVegetablePoints = -5; 
    public TMP_Text scoreText; 

    private void OnMouseDown()
    {
        if (isVegetable)
        {
            FoundManager.instance.AddScore(vegetablePoints);
            FoundManager.instance.VegetableFound(); 
        }
        else
        {
            FoundManager.instance.AddScore(nonVegetablePoints);
        }

        Destroy(gameObject); 
    }
}
