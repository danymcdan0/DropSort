//--------------------------------------------------------------------------------------------------------------------\\
//Score.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to handle the score value and bool to switch conditions for checkpoints:
//--------------------------------------------------------------------------------------------------------------------\\
//-->Score keeps track of the Players score to display on the top right with TextMeshProUGUI scoreCounter.
//-->The Update() function first checks the socrechecker list to see if the score is new and adds it to the list, this
//is done to ensure that while the score is the same it does not constantly change the MyCoroutine() delayChange bool.
//Next a check is done to see if the score is a multiple of 50 with "score % 50 == 0 && score != 0" and if it is then
//the mainManager SpeechBubbles() function is called to display, Speech() function to change the text indicating
//direction according to the checker bool and the MyCoroutine() function is called.
//-->The checkpoint scripts refer to the delayChange bool.
//-->The purpose of having a delayChange bool is as when the AI is already past the "barrier" and the conditions for the
//checkpoint change the Player would lose health, hence using IEnumerator MyCoroutine() and the WaitForSeconds(4) allows
//for the checkpoints to be delayed and accept AI that has already passed the "barrier". This makes the game more fair.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score = 0;
    private TextMeshProUGUI scoreCounter; //TMP object
    public static bool checker = true; //Standard bool for making sure the conditions invert
    public static bool delayChange = true; //Bool checked to by the checkpoint scripts
    public MainManager mainManager;
    public List<int> socrechecker = new List<int>(); //To store all the score values

    private void Awake()
    {
        score = 0; //When a new game starts set the score to 0
        scoreCounter = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        scoreCounter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        scoreCounter.text = score.ToString();
        if (!socrechecker.Contains(score)) //Check if the score is new
        {
            socrechecker.Add(score);
            if (score % 50 == 0 && score != 0) //Math
            {
                Debug.LogWarning("Score is " + score + " change direction!");
                mainManager.SpeechBubbles();
                mainManager.Speech();
                StartCoroutine(MyCoroutine());
            }
            checker = !checker;
        }

    }
    IEnumerator MyCoroutine() //Coroutine here to change a different bool that checkpoint1+2 are refering to
    {

        yield return new WaitForSeconds(4); //Amount of time it takes for AI to go from barrier to checkpoints

        delayChange = !delayChange;

    }
}
