using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button1Click : MonoBehaviour
{
    public GameObject[] buttons;

    private void Start()
    {
        foreach (GameObject button in buttons)
        {
            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OnButtonClick(button));
            }
            else
            {
                Debug.LogWarning($"GameObject {button.name} does not have a Button component.");
            }
        }
    }

    private void OnButtonClick(GameObject clickedButton)
    {
        // Disable the Button component to prevent further clicks
        Button buttonComponent = clickedButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.enabled = false;
        }

        // Start the coroutine to re-enable the Button component after a delay
        StartCoroutine(ReenableButtonAfterDelay(clickedButton, 3f));
    }

    private IEnumerator ReenableButtonAfterDelay(GameObject buttonToEnable, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Re-enable the Button component
        Button buttonComponent = buttonToEnable.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.enabled = true;
        }
    }
}
