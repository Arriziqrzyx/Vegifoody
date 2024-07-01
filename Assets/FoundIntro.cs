using UnityEngine;
using System.Collections;

public class FoundIntro : MonoBehaviour
{
    public GameObject objectToActivate;
    public float startDelay = 2.5f;

    void Start()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    private IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);

        objectToActivate.SetActive(true);
    }
}
