using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpUIHandler : MonoBehaviour
{
    private readonly string mainMenuSceneName = "MainMenuScene";
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
