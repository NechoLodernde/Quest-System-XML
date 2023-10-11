using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    
    public Player player;

    public GameObject questWindow;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text experienceText;
    public TMP_Text goldText;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }
}
