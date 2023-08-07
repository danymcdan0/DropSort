//--------------------------------------------------------------------------------------------------------------------\\
//Trigger.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to govern the direction to send AI:
//--------------------------------------------------------------------------------------------------------------------\\
//-->Trigger is the script that holds the initial bool that all other scripts that handle AI movement refer to, which
//direction to send the AI in.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public static bool direction = true;
    public AudioSource Ding; //Sound to play when the direction is changed

    public void playDing()
    {
        Ding.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") //Only the Player is allowed to change the direction
        {
            playDing();
            direction = !direction; //Inverts the bool for 'left' and 'right' respectively
        }
    }
}
