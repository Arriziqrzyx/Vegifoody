using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButtonTyping : MonoBehaviour
{
    [SerializeField] private Button targetButton; // Tombol target yang akan dinonaktifkan
    [SerializeField] private GameObject[] dialogObjects; // Array objek dialog

    void Update()
    {
        CheckDialogs();
    }

    private void CheckDialogs()
    {
        bool anyDialogActive = false;

        foreach (GameObject dialog in dialogObjects)
        {
            if (dialog.activeInHierarchy)
            {
                anyDialogActive = true;
                break;
            }
        }

        targetButton.interactable = !anyDialogActive;
    }
}
