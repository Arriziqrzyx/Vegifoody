using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MateriManager : MonoBehaviour
{

    [SerializeField] private string Scene;

    public void BackToGameplay()
    {
        SceneManager.UnloadSceneAsync(Scene);
    }
    
}
