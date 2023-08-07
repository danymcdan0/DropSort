//--------------------------------------------------------------------------------------------------------------------\\
//UIManager.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script to handle interactions in the Menu scene:
//--------------------------------------------------------------------------------------------------------------------\\
//-->UIManager was made into a Singleton to ensure that the FirebaseManager script has access to all the input fields in
//the Menu scene even when going between scenes and is referred to from the FirebaseManager script as
//UIManager.instance.requiredField.text to get the text that the UIManager has stored in the scene.
//-->This script handles when buttons are pressed as when going between scene multiple times the FirebaseManager must
//know when functions should be called hence, .onClick.AddListener(FindObjectOfType<FirebaseManager>().ButtonHere) looks
//for the object with FirebaseManager in it and then calls the button click method.
//-->This script handles which UI to display, first clearing the current UI with ClearScreen() and displaying the
//needed UI according to the button clicked with other functions.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //Login Variables
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    //Warning Display Variables
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    //Registration Field Variables
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    public GameObject loginUI; //Login UI for displaying 
    public GameObject registerUI; //Register UI for displaying 
    public GameObject menuUI; //Menu UI for displaying 
    public GameObject scoreboardUI; //Scoreboard UI for displaying 
    public GameObject settingsUI; //Settings UI for displaying
    public Button scoreButton; //Scoreboard for FirebaseManager to be called
    public Button SignoutButton; //Sign out for FirebaseManager to be called
    public Button loginButton; //Login for FirebaseManager to be called
    public Button registerButton; //Register for FirebaseManager to be called

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        if (FindObjectOfType<FirebaseManager>().first == false){
            ClearScreen();
            MenuScreen();
        }
    }

    //Functions to change the login screen UI

    public void ClearScreen() //Turn off all screens
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        menuUI.SetActive(false);
        scoreboardUI.SetActive(false);
        settingsUI.SetActive(false);
    }

    public void LoginScreen() //Login Screen UI to display
    {
        ClearScreen();
        loginUI.SetActive(true);
    }
    public void RegisterScreen() //Register Screen UI to display
    {
        ClearScreen();
        registerUI.SetActive(true);
    }

    public void MenuScreen() //Logged in
    {
        ClearScreen();
        menuUI.SetActive(true);
    }

    public void ScoreboardScreen() //Scoreboard button
    {
        ClearScreen();
        scoreboardUI.SetActive(true);
    }

    public void SettingsScreen()
    {
        ClearScreen();
        settingsUI.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    void Start () { //Initializing the buttons to be listened for
        Button b = scoreButton.GetComponent<Button>();
        b.onClick.AddListener(FindObjectOfType<FirebaseManager>().ScoreboardButton);
        Button s = SignoutButton.GetComponent<Button>();
        s.onClick.AddListener(FindObjectOfType<FirebaseManager>().SignOutButton);
        Button l = loginButton.GetComponent<Button>();
        l.onClick.AddListener(FindObjectOfType<FirebaseManager>().LoginButton);
        Button r = registerButton.GetComponent<Button>();
        r.onClick.AddListener(FindObjectOfType<FirebaseManager>().RegisterButton);
    }
    
}
