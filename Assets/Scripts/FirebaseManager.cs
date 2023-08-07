//--------------------------------------------------------------------------------------------------------------------\\
//FirebaseManager.cs
//--------------------------------------------------------------------------------------------------------------------\\
//PURPOSE: Script handles all interactions with Firebase:
//--------------------------------------------------------------------------------------------------------------------\\
//-->FirebaseManager was made into a Singleton to ensure that there are no duplicates of the object with a combination
//of DontDestroyOnLoad(this.gameObject) to ensure when going between scenes that the same FirebaseManager is in use
//remembering who is logged in.
//-->This script has mainly button interactions which are called from either the MainManager or UIManager which then 
//call local IEnumerator functions to handle the interactions with the Firebase Database.
//-->Firebase manager refers to the MainManager and UIManager Singleton instances to be able to get various variable
//values such as email and password.
//-->The MainManager Singleton use is to get the score value: MainManager.Singleton.score.text
//-->The UIManager Singleton use is to get all other values entered in the Menu scene fields.
//--------------------------------------------------------------------------------------------------------------------\\
//AUTHOR: DANYAL SALEH ds18635
//Registration number: 1806262
//--------------------------------------------------------------------------------------------------------------------\\
using System;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth; //Firebase authentication services
    public FirebaseUser User; //To save current user being handled 
    public DatabaseReference DBreference;
    public GameObject scoreElement; //Object to hold the score and time
    public GameObject scoreboardContent; //Object to display the scoreboardContent

    DateTime dateTime = DateTime.Now;
    public bool first = true; //Bool to keep track if it is the first user
    
    private static FirebaseManager _instance;
    
    public static FirebaseManager Singleton
    {
        get => _instance;
        private set
        {
            if (_instance == null) //Checks if there is no instance of FirebaseManager
            {
                _instance = value; //Sets the instance to value for further checks
            }
            else if (_instance != value) //Checks if there is an instance of FirebaseManager that is not value
            {
                Debug.Log($"{nameof(FirebaseManager)} instance already exists, destroying object!");
                Destroy(value.gameObject); //Destroys the object to ensure no duplicates
            }
        }
    }
    void Awake()
    {
        Singleton = this; //Sets the singleton
        DontDestroyOnLoad(this.gameObject);
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance; //Setting the authentication instance 
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void ClearLoginFeilds()
    {
        UIManager.instance.emailLoginField.text = "";
        UIManager.instance.passwordLoginField.text = "";
    }
    public void ClearRegisterFeilds()
    {
        UIManager.instance.usernameRegisterField.text = "";
        UIManager.instance.emailRegisterField.text = "";
        UIManager.instance.passwordRegisterField.text = "";
        UIManager.instance.passwordRegisterVerifyField.text = "";
    }
    
    public void LoginButton() //Call the login coroutine passing email and password
    {
        StartCoroutine(Login(UIManager.instance.emailLoginField.text, UIManager.instance.passwordLoginField.text));
    }
    public void RegisterButton() //Call the register coroutine passing the email, password, and username
    {
        StartCoroutine(Register(UIManager.instance.emailRegisterField.text, UIManager.instance.passwordRegisterField.text, UIManager.instance.usernameRegisterField.text));
    }
    public void SignOutButton() //Call the signout function + set the scene to display Login canvas and clear all fields
    {
        auth.SignOut();
        UIManager.instance.LoginScreen();
        ClearRegisterFeilds();
        ClearLoginFeilds();
        first = true;
    }
    public void SendScore() //Call the UpdateScore coroutine passing the score held in MainManager Singleton
    {
        dateTime = DateTime.Now; //Set the date and time for the score to be saved with
        StartCoroutine(UpdateScore(int.Parse(MainManager.Singleton.score.text)));
    }
    public void ScoreboardButton() //Call the LoadScoreboardData coroutine
    {
        StartCoroutine(LoadScoreboardData());
    }

    private IEnumerator Login(string email, string password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted); //Wait for the task to complete

        if (LoginTask.Exception != null) //Error handling
        {
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseError = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError error = (AuthError)firebaseError.ErrorCode;

            string message = "Login Failed!";
            if (error == AuthError.MissingEmail)
                message = "Missing Email";
            else if (error == AuthError.MissingPassword)
                message = "Missing Password";
            else if (error == AuthError.WrongPassword)
                message = "Wrong Password";
            else if (error == AuthError.InvalidEmail)
                message = "Invalid Email";
            else if (error == AuthError.UserNotFound) message = "Account does not exist";

            UIManager.instance.warningLoginText.text = message; //Set the message according to the error
        }
        else
        {
            User = LoginTask.Result; //User is now set after successful logged in
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            UIManager.instance.warningLoginText.text = "";
            UIManager.instance.confirmLoginText.text = "Logged In";
            yield return new WaitForSeconds(2); //To display the login status
            UIManager.instance.MenuScreen(); //Change the UI canvas to mainmenu after login then clear UI fields
            UIManager.instance.confirmLoginText.text = "";
            ClearLoginFeilds();
            ClearRegisterFeilds();
        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if (username == "") //Checks for if username has been entered
        {
            UIManager.instance.warningRegisterText.text = "Missing Username!";
        }
        //Checks for if password has been entered correctly
        else if(UIManager.instance.passwordRegisterField.text != UIManager.instance.passwordRegisterVerifyField.text)
        {
            UIManager.instance.warningRegisterText.text = "Password Does Not Match!";
        }
        else 
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted); //Wait for task completion

            if (RegisterTask.Exception != null)
            {
                //Error Handling
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseError = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseError.ErrorCode;
                string message = "Register Failed!";
                if (error == AuthError.MissingEmail)
                    message = "Email Missing!";
                else if (error == AuthError.MissingPassword)
                    message = "Password Missing!";
                else if (error == AuthError.WeakPassword)
                    message = "Weak Password!";
                else if (error == AuthError.EmailAlreadyInUse) message = "Email In Use!";

                UIManager.instance.warningRegisterText.text = message; //Set the message according to the error
            }
            else
            {
                User = RegisterTask.Result; //User set after correctly registering

                if (User != null)
                {
                    UserProfile profile = new UserProfile{DisplayName = username}; //Creating user profile and set the username
                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted); //Wait for task completion

                    if (ProfileTask.Exception != null) //Error checking
                    {
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        UIManager.instance.warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        UIManager.instance.LoginScreen(); //Return to login screen + reset fields
                        UIManager.instance.warningRegisterText.text = "";
                        ClearRegisterFeilds();
                        ClearLoginFeilds();
                    }
                }
            }
        }
    }

    private IEnumerator UpdateScore(int score) //Update the currently logged in user score
    {
        first = false;
        //Formatting date and time as Firebase recognizes "/" as a new child
        string finaldatetime = dateTime.ToString().Replace("/", ":");
        //Save the score and time in the correct location in the Firebase database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("scores").Child(finaldatetime).Child("score").SetValueAsync(score);
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted); //Wait for task completion

        if (DBTask.Exception != null) //Error handling
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    private IEnumerator LoadScoreboardData()
    {
        //Get all the users data ordered by score amount
        UIManager.instance.ScoreboardScreen();
        //Get the reference to the database and order by highest score
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("scores").OrderByChild("score").GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted); //Wait for task completion
        if (DBTask.Exception != null) //Error handling 
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = DBTask.Result; //Data snapshot saved for displaying
            scoreboardContent = GameObject.Find("Content"); //Locate the object to display in
            foreach (Transform child in scoreboardContent.transform) //Destroy any existing scoreboard elements
            {
                Destroy(child.gameObject);
            }
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())//Loop through the users scores
            {
                string time = childSnapshot.Key.ToString();
                int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent.transform);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(time, score);
            }
        }
    }
}
//For understanding how to connect Unity and FirebaseManager:
//https://www.youtube.com/watch?v=NsAUEyA2TRo&list=PL_OuuwK_JzLnsBehAJdkG3iRxp1qcwDg5&index=30
//Use of the Firebase and Unity documentation:
//https://firebase.google.com/docs/unity/setup
