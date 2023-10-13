using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUIHandler : MonoBehaviour
{
    private readonly string gameplaySceneName = "GameplayScene";
    private readonly string playerCreationSceneName = "PlayerCreationScene";

    public void GoToGameplay()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void GoToPlayerCreation()
    {
        SceneManager.LoadScene(playerCreationSceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
