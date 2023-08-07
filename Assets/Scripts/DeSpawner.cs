//--------------------------------------------------------------------------------------------------------------------//
//DeSpawner.cs
//--------------------------------------------------------------------------------------------------------------------//
//PURPOSE: Script handles removing AI that have served their purpose:
//--------------------------------------------------------------------------------------------------------------------//
//-->The Update() function checks for when the objects position and removes the object this script is attached to
//when the transform.position.z <= -1
//--------------------------------------------------------------------------------------------------------------------//
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------//
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DeSpawner : MonoBehaviour
{
    void Update()
    {
        if (this.transform.position.z <= -1)
        {
            Remove();
        }
    }
    void Remove()
    {
        Destroy(this.gameObject);
    }
}
