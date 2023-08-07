//--------------------------------------------------------------------------------------------------------------------//
//EndScore.cs
//--------------------------------------------------------------------------------------------------------------------//
//PURPOSE: Script handles displaying the score in the GameOver canvas:
//--------------------------------------------------------------------------------------------------------------------//
//-->The Update() function sets the "current" variable to the Score.score value, converts the int to a string then
//stores it in the "finalScore" variable. This is done as a TMP_Text .SetText only takes strings. This is done every
//frame to ensure the GameOver score displayed is correct even when changing.
//--------------------------------------------------------------------------------------------------------------------//
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------//

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScore : MonoBehaviour
{
    public TMP_Text gameoverScore;
    private int current;
    private string finalScore;
    

    private void Update()
    {
        current = Score.score;
        finalScore = current.ToString();
        gameoverScore.SetText(finalScore);
    }
}
