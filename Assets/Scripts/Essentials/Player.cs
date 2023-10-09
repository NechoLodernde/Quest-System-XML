using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int experience;
    public int gold;

    private void Start()
    {
        health = 5;
        experience = 40;
        gold = 1000;
    }

    public void GoBattle()
    {
        health -= 1;
        experience += 2;
        gold += 5;
    }
}
