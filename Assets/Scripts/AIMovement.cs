//-------------------------------------------------------------------------------------------------------------------\\
//PlayerMovement.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to control the movement of the Player:
//--------------------------------------------------------------------------------------------------------------------\\
//-->The Update() function checks when the mouse button is clicked then uses the camera to send a ray, which if it
//intersects with a Collider, sets the Players destination to move to.
//--> .Raycast = Boolean to Check if the location the mouse was clicked is a Collider.
//--> .ScreenPointToRay = Returns a ray from the camera through a screen point going through points x,y which the Player
//wants to move
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;

public class AIMovement : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent; //Refering to the Player object
    public bool active = true;
    
    public void Stop() //For when the game ends, to stop the control of the Player.
    {
        active = false;
    }

    void Update()
    {
        if (active == true) {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition); 
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))  
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}
