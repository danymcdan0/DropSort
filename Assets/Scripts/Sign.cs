//--------------------------------------------------------------------------------------------------------------------\\
//Sign.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to change the signs color random AI spawning:
//--------------------------------------------------------------------------------------------------------------------\\
//-->This script refers to the Trigger.direction bool to change colors to indicate which direction the AI will go to 
//the player. This color matches the color of the color of the roofs.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private Renderer render; //To be able to change color
    bool SignDirection;
    Color32 left = new Color32(255, 114, 0, 255); //Color for the left house
    Color32 right = new Color32(117, 38, 204, 255); //Color for the right house

    private void Update()
    {
        SignDirection = Trigger.direction; //To be in sync with the Trigger bool
        if (SignDirection == false)
        {
            changeColor();
        }
        if (SignDirection == true)
        {
            changeColor();
        }
    }
    void changeColor() //Change color depending on bool
    { 
        render = GetComponent<Renderer>();
        render.enabled = true;
        transform.GetComponent<Renderer>().material.color = SignDirection ? left : right; 
    }
}
