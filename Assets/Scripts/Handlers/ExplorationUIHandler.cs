using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ExplorationUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject cameraObject;
    public PlayerActions controls;

    private readonly string mainMenuSceneName = "MainMenuScene";

    private void Awake()
    {
        controls = new PlayerActions();
    }

    private void Update()
    {
        if (controls.PlayerMain.Pause.triggered)
        {
            ResumeGame();
        }
    }

    private void OnEnable()
    {
        cameraObject.SetActive(false);
        controls.Enable();
    }

    private void OnDisable()
    {
        cameraObject.SetActive(true);
        controls.Disable();
    }

    public void ResumeGame()
    {
        this.gameObject.SetActive(false);
        playerObject.SetActive(true);
    }

    public void QuitGame()
    {
        PlayerXMLManager.PlayerXMLInstance.ClearList();
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
