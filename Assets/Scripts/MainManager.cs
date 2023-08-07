//--------------------------------------------------------------------------------------------------------------------\\
//MainManager.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to handle interactions in the Game scene:
//--------------------------------------------------------------------------------------------------------------------\\
//-->MainManager was made into a Singleton to ensure that the FirebaseManager script has access to the elements of the
//Game scene and get the score the Player achieves.
//-->This script handles when speech bubbles are displayed which is how to player knows which side accepts which AI. The
//speech bubbles change sprites to catch the players attention and change the text displayed according to the direction
//bool which kept in sync with the Score.checker bool by setting it in Update().
//-->This script handles ending all movement happening in the game when the Health script calls EndGame(), this function
//turns a bool false for all other scripts stopping movement functions being called.
//-->Further when the menu button is pressed to go back to the Menu scene, the sendFB() function is called which tells
//FirebaseManager to send the score to the database.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public AIMovement playerMovement; //Player for EndGame() function
    public TimedSpawner spawner; //AI spawning for EndGame() function
    public GameObject GameOverUI; //UI for when the game ends
    public GameObject SpeechUI; //UI for speech bubbles
    public TMP_Text speechL;
    public TMP_Text speechR;
    public TMP_Text score;
    public static bool direction;
    public AudioSource Interact; //Sound for when speech bubbles appear
    public Image imageL;
    public Image imageR;
    public Sprite[] spriteLeft; //To hold the sprites for the left bubble
    public Sprite[] spriteRight; //To hold the sprites for the right bubble
    

    private static MainManager _instance;

    public static MainManager Singleton
    {
        get
        { 
           return  _instance;
        } 
    }

    private void Awake()
    {
        if (_instance != null && _instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    public void Start()
    {
        SpeechBubbles(); //Display which side when the game starts
    }

    public void Update()
    {
        direction = Score.checker; //Keep the direction bool in sync with Score.checker
    }
    
    public void EndGame()
    {
        Debug.Log("Game Over");
        playerMovement.Stop();
        spawner.Stop();
        FindObjectOfType<AIMovementPoint>().Stop();
        GameOverUI.SetActive(true);
    }

    public void playInteract()
    {
        Interact.Play();
    }

    public void sendFB()
    {
        if (score.text != "0") { //So no value of 0 is sent
            FindObjectOfType<FirebaseManager>().SendScore();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //Go back to the Menu scene
    }

    public void SpeechBubbles()
    {
        StartCoroutine(MyCoroutine());
    }
    
    IEnumerator MyCoroutine() //Coroutine to make speech bubble sprite changes over time
    {
        SpeechUI.SetActive(true);
        playInteract();
        yield return new WaitForSeconds(1);
        imageL.sprite = spriteLeft[1];
        imageR.sprite = spriteRight[1];
        yield return new WaitForSeconds(1);
        imageL.sprite = spriteLeft[0];
        imageR.sprite = spriteRight[0];
        yield return new WaitForSeconds(1);
        imageL.sprite = spriteLeft[1];
        imageR.sprite = spriteRight[1];
        yield return new WaitForSeconds(1);
        imageL.sprite = spriteLeft[0];
        imageR.sprite = spriteRight[0];
        SpeechUI.SetActive(false);
        
    }

    public void Speech() //.text to set the speech bubbles text according to the direction bool
    {
        if (direction)
        {
            speechL.text = "Hey! Green Drops Only Please!";
            speechR.text = "Hey! Grey Drops Only Please!";
        }

        if (!direction)
        {
            speechL.text = "Hey! Grey Drops Only Please!";
            speechR.text = "Hey! Green Drops Only Please!";
        }
    }
}
