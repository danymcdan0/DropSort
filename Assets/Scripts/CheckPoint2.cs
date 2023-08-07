//--------------------------------------------------------------------------------------------------------------------//
//CheckPoint2.cs
//--------------------------------------------------------------------------------------------------------------------//
//PURPOSE: Script handles collisions with the AI object and increase points or lose health respectively on the RIGHT:
//--------------------------------------------------------------------------------------------------------------------//
//-->The Update() function sets the Checker bool to score.delayChange. This bool controls which side accepts which AI as
//this changes when a score milestone is reached.
//-->If the AI is correct the Score.score variable gets updated to increase by 10.
//-->If the AI is incorrect the hp.loseHealth() function is called to reduce health by 1.
//--------------------------------------------------------------------------------------------------------------------//
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------//
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CheckPoint2 : MonoBehaviour
{
    public Health hp;
    public bool Checker;
    public AudioSource Point; //Audio to play when correct AI
    public AudioSource Hurt; //Audio to play when incorrect AI

    public void Update()
    {
        Checker = Score.delayChange; //In delayed sync with Score's bool.
    }
    
    public void playPoint()
    {
        Point.Play();
    }    
    public void playHurt()
    {
        Hurt.Play();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name + " Has Touched The CheckPoint");
        if (Checker)
        {
            if (collision.gameObject.name == "Sort AI 2" || collision.gameObject.name == "Sort AI 2(Clone)")
            {
                Score.score += 10;
                playPoint();
            }

            if (collision.gameObject.name == "Sort AI" || collision.gameObject.name == "Sort AI(Clone)")
            {
                hp.loseHealth();
                playHurt();
            }
        }

        if (!Checker)
        {
            if (collision.gameObject.name == "Sort AI" || collision.gameObject.name == "Sort AI(Clone)")
            {
                Score.score += 10;
                playPoint();
            }

            if (collision.gameObject.name == "Sort AI 2" || collision.gameObject.name == "Sort AI 2(Clone)")
            {
                hp.loseHealth();
                playHurt();
            }
        }
    }
}