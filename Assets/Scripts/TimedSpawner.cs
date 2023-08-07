//--------------------------------------------------------------------------------------------------------------------\\
//TimedSpawner.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to handle random AI spawning:
//--------------------------------------------------------------------------------------------------------------------\\
//-->TimedSpawner makes use of InvokeRepeating to spawn the AI at a chosen time and delay until the InvokeRepeating is
//stopped with CancelInvoke, which is done by calling the Stop() function when the game ends. 
//-->SpawnAI() makes use of Random to chose a random position in the targets[] array and then spawns the random AI with
//Instantiate until the stopSpawner bool = true.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawner : MonoBehaviour
{
    public GameObject[] targets; //To hold the two type of AI
    public bool stopSpawner = false; //bool for when game ends to not spawn more
    public float spawnerTime;
    public float spawnerDelay;
    public int randomTarget; //Int for random AI choosing

    void Start()
    {
        stopSpawner = false;
        InvokeRepeating(nameof(SpawnAI), spawnerTime, spawnerDelay);
    }

    public void Stop()
    {
        stopSpawner = true;
    }

    public void SpawnAI()
    {
        randomTarget = Random.Range(0, targets.Length); //Use of Random to chose a random position in the targets[] 
        Instantiate(targets[randomTarget], transform.position, transform.rotation);//Creating the AI at chosen position
        if (stopSpawner)
        {
                CancelInvoke(nameof(SpawnAI));
        }
    }
}


//For understanding who to chose a random game object:
//https://forum.unity.com/threads/how-to-pick-a-random-gameobject.433608/