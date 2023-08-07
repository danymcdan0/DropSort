//--------------------------------------------------------------------------------------------------------------------\\
//AIMovementPoint.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to control the movement of the AI:
//--------------------------------------------------------------------------------------------------------------------\\
//-->The bool checkDirection is set to the same value as the bool Tigger.direction in Update(). The Trigger script
//sets the bool direction that the Player wants hence the bool true or false represents left or right respectively
//--> .SetDestination is different according to the checkDirection bool.
//-->When the AI Collides with the "barrier" object the OnTriggerExit() function occurs which sets the AI direction when
//the collision ends allowing for last second changes in direction.
//-->After the "barrier" collision, when the AI Collides with the "checkpoint1"/"checkpoint2" objects the
//OnTriggerExit() function occurs which sets the AI direction to a different set of coordinates depending on which side
//the AI is on to Despawn the objects.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementPoint : MonoBehaviour
{
    public NavMeshAgent agent; //Refering to the AI object
    public Vector3 direction; //Holds the direction to set the AI to
    bool checkDirection;
    bool active = true;

    private void Start()
    {
        direction = new Vector3(12f, 0f, 15f);
        agent.SetDestination(direction);
    }

    void Update()
    {
        checkDirection = Trigger.direction; //In sync with Trigger's direction bool.
    }

    public void Stop() //For when the game ends, to stop the movement of the AI.
    {
        active = false;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (active == true)
        {
            if (collision.gameObject.name == "Barrier")
            {
                if (checkDirection)
                {
                    direction = new Vector3(-7f, 0f, -3f);
                    agent.SetDestination(direction);
                }
                else
                {
                    direction = new Vector3(30f, 0f, -3f);
                    agent.SetDestination(direction);
                }

            }

            if (collision.gameObject.name == "Checkpoint1")
            {
                direction = new Vector3(10.5f, 0f, 12f);
                agent.SetDestination(direction);
            }

            if (collision.gameObject.name == "Checkpoint2")
            {
                direction = new Vector3(-25.5f, 0f, 12f);
                agent.SetDestination(direction);
            }
        }
    }
}
