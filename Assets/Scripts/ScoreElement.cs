//--------------------------------------------------------------------------------------------------------------------\\
//ScoreElement.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to hold the time and score object for the scoreboard:
//--------------------------------------------------------------------------------------------------------------------\\
//-->Purpose of this script is for FirebaseManager to be able to refer to this object when the Scoreboard need to be
//displayed. Just to hold those two elements as an object
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text dateText;
    public TMP_Text scoreText;

    public void NewScoreElement (string time, int score)
    {
        dateText.text = time;
        scoreText.text = score.ToString();
    }

}