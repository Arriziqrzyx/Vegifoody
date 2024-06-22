using UnityEngine;
using TMPro;

public class VegetableObject : MonoBehaviour
{
    public bool isVegetable = true; // Set true if this object is a vegetable, false if it is not
    public int vegetablePoints = 10; // Points added if it's a vegetable
    public int nonVegetablePoints = -5; // Points deducted if it's not a vegetable
    public TMP_Text scoreText; // Drag and drop your TMP_Text element for displaying score here in the Inspector

    private void OnMouseDown()
    {
        if (isVegetable)
        {
            FoundManager.instance.AddScore(vegetablePoints);
            FoundManager.instance.VegetableFound(); // Call method to check if all vegetables are found
        }
        else
        {
            FoundManager.instance.AddScore(nonVegetablePoints);
        }

        Destroy(gameObject); // Destroy the clicked object
    }
}
