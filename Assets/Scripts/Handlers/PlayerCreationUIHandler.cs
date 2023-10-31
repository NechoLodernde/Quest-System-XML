using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerCreationUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public ToggleGroup toggleGroup;
    public TMP_Dropdown roleInputField;

    private readonly string mainMenuSceneName = "MainMenuScene";
    private readonly string explorationSceneName = "ExplorationScene";

    // Start is called before the first frame update
    private void Start()
    {
        var roleDropDown = roleInputField.transform.GetComponent<TMP_Dropdown>();
        List<string> roleOptions;
        roleOptions = PlayerXMLManager.PlayerXMLInstance.GetRolesData();
        roleDropDown.AddOptions(roleOptions);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void GoToExploration()
    {
        SceneManager.LoadScene(explorationSceneName);
    }

    public void SubmitData()
    {
        var inputDropDown = roleInputField.transform.GetComponent<TMP_Dropdown>();
        int index = inputDropDown.value;
        List<TMP_Dropdown.OptionData> roleOptions = inputDropDown.options;
        string playerName = nameInputField.text.ToString();
        string playerRole = roleOptions[index].text.ToString();
        string playerGender = "";
        // 1st method to get active toggle from toggle group
        //foreach (Toggle toggle in toggleGroup.ActiveToggles())
        //{
        //    if (playerGender.Length > 0)
        //    {
        //        break;
        //    }
        //    Debug.Log(toggle);
        //    switch (toggle.ToString())
        //    {
        //        case "ToggleMale":
        //            playerGender = "Male";
        //            break;
        //        case "ToggleFemale":
        //            playerGender = "Female";
        //            break;
        //        default:
        //            Debug.Log("No active toggle");
        //            break;
        //    }
        //}

        // 2nd method
        Toggle selectedToggle = toggleGroup.ActiveToggles().FirstOrDefault();
        if (selectedToggle != null)
        {
            //Debug.Log(selectedToggle);
            switch (selectedToggle.name)
            {
                case "ToggleMale":
                    playerGender = "Male";
                    break;
                case "ToggleFemale":
                    playerGender = "Female";
                    break;
                default:
                    Debug.Log("No selected toggle");
                    break;
            }
        }
        //Debug.Log(playerName);
        //Debug.Log(playerRole);

        // Function to get active toggle from a toggle group
        //foreach(Toggle toggle in toggleGroup.ActiveToggles())
        //{
        //    Debug.Log(toggle);
        //}
        PlayerXMLManager.PlayerXMLInstance.playerDB.list.Clear();
        PlayerEntry newEntry = new();
        newEntry.playerName = playerName;
        newEntry.playerGender = PlayerXMLManager.PlayerXMLInstance.GetGender(playerGender);
        newEntry.playerRole = PlayerXMLManager.PlayerXMLInstance.GetRole(playerRole);
        newEntry.playerLevel = 1;
        newEntry.playerExp = 0;
        PlayerXMLManager.PlayerXMLInstance.playerDB.list.Add(newEntry);
        PlayerXMLManager.PlayerXMLInstance.SavePlayerData();
        GoToExploration();
    }
}
