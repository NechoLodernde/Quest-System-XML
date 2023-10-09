using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayer : MonoBehaviour
{
    public Player player;
    public TMP_Text health_text, experience_text, gold_text;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        health_text.text = player.health.ToString();
        experience_text.text = player.experience.ToString();
        gold_text.text = player.gold.ToString();
    }
}
