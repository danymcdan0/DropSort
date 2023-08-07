//--------------------------------------------------------------------------------------------------------------------//
//Health.cs
//--------------------------------------------------------------------------------------------------------------------//
//PURPOSE: Script handles the health sprite displayed, the health the player has and when the game ends:
//--------------------------------------------------------------------------------------------------------------------//
//-->The Update() function handles all sprite changes depending on the health value, if the Player health is 2 when the
//total health is 3, the game would have 2 full health sprites and one empty heart sprite.
//-->Keeps track of the Players current health to be able to end the game correctly.
//--------------------------------------------------------------------------------------------------------------------//
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health; //Initial health
    public int totalHearts; //Total health
    
    public Image[] hearts; //Array to hold sprites
    public Sprite fullHeart; //Sprite for when health present
    public Sprite emptyHeart; //Sprite for when health lost
    private static bool checker = true; //To make sure the game ends properly
    public MainManager mainManager;

    public void loseHealth() //Function called by checkpoint scripts when health is to be lost
    {
        health -= 1;
    }
    
    public void Awake()
    {
        checker = true; //When a new game starts
    }

    public void Update()
    {
        if (health > totalHearts) //To make sure that health is not more than the total health
        {
            health = totalHearts;
        }
        for (int i = 0; i < hearts.Length; i++) //Loop to update the sprites to display depending on health value
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < totalHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if (health == 0) //When health hits 0 to call the mainManager.EndGame();
        {
            if (checker)
            {
                mainManager.EndGame();
                checker = false; //To ensure that it is only called once 
            }
        }
    }
}
