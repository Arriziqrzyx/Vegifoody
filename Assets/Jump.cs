using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour, IPointerDownHandler
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found in the scene.");
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerController != null)
        {
            playerController.loncat();
        }
    }
}
