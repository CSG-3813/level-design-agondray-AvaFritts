/**** 
 * Created by: Akram Taghavi-Burrs
 * Date Created: Feb 23, 2022
 * 
 * Last Edited by: Ava Fritts
 * Last Edited: Dec 13th, 2022
 * 
 * Description: Basic GameManager Template
****/

/** Import Libraries **/
using System; //C# library for system properties
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //libraries for accessing scenes


public class GameManager : MonoBehaviour
{
    /*** VARIABLES ***/

    #region GameManager Singleton
    static private GameManager gm; //refence GameManager
    static public GameManager GM { get { return gm; } } //public access to read only gm 

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckGameManagerIsInScene()
    {

        //Check if instnace is null
        if (gm == null)
        {
            gm = this; //set gm to this gm of the game object
            Debug.Log(gm);
        }
        else //else if gm is not null a Game Manager must already exsist
        {
            Destroy(this.gameObject); //In this case you need to delete this gm
        }
        DontDestroyOnLoad(this); //Do not delete the GameManager when scenes load
        Debug.Log(gm);
    }//end CheckGameManagerIsInScene()
    #endregion

    [Header("GENERAL SETTINGS")]
    public string gameTitle = "Untitled Game";  //name of the game
    public string gameCredits = "Made by Me"; //game creator(s)
    public string copyrightDate = "Copyright " + thisDay; //date created

    [Header("GAME SETTINGS")]

    [SerializeField] //Access to private variables in editor
    [Tooltip("Check to test player lost the level")]
    private bool levelLost = false;//we have lost the level (ie. player died)
    public bool LevelLost { get { return levelLost; } set { levelLost = value; } } //access to private variable lostLevel [get/set methods]

    [Space(10)]
    public string defaultEndMessage = "Game Over";//the end screen message, depends on winning outcome
    public string loseMessage = "You Lose"; //Message if player loses
    public string winMessage = "You Win"; //Message if player wins
    [HideInInspector] public string endMsg;//the end screen message, depends on winning outcome

    [Header("SCENE SETTINGS")]

    [Tooltip("Name of the start scene")]
    public string startScene;

    [Tooltip("Name of the Credits scene")]
    public string creditsScene;


    [Tooltip("Name of the game over scene")]
    public string gameOverScene;

    [Tooltip("Count and name of each Game Level (scene)")]
    public string[] gameLevels; //names of levels
    [HideInInspector]
    public int gameLevelsCount; //what level we are on
    private int loadLevel; //what level from the array to load

    public static string currentSceneName; //the current scene name;

    [Header("FOR TESTING")]
    public bool nextLevel = false; //test for next level
    public bool isTesting = false; //used to allow me to test game stuff without having to deal with the different cameras and such.


    //Game State Variables
    [HideInInspector] public enum gameStates { Idle, Playing, GameOver, BeatLevel };//enum of game states
    [HideInInspector] public gameStates gameState = gameStates.Idle;//current game state

    //Timer Variables
    private float currentTime; //sets current time for timer
    private bool gameStarted = false; //test if games has started

    //Win/Lose conditon
    //[SerializeField] //to test in inspector
    public bool playerWon = false; //changed from private to public in hopes of making the game winable.

    //reference to system time
    private static string thisDay = System.DateTime.Now.ToString("yyyy"); //today's date as string


    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start). Awake only once during the lifetime of the script instance.
    void Awake()
    {
        //runs the method to check for the GameManager
        CheckGameManagerIsInScene();

        //store the current scene
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        //Get the saved high score
        //GetHighScore();

    }//end Awake()


    // Update is called once per frame
    private void Update()
    {
        //if ESC is pressed, pause game
        if (Input.GetKey("escape")) { Application.Quit(); }

        //Check for next level
        if (nextLevel) { NextLevel(); }

        //if we are playing the game
        if (gameState == gameStates.Playing)
        {
            //if we have died or overcame the boss, go to game over screen
            if (levelLost || playerWon) { GameOver(); }

        }//end if (gameState == gameStates.Playing)

        //Check Score
        //CheckScore();

    }//end Update

    //START LEVEL SELECT
    public void StartGame()
    {
       SceneManager.LoadScene(startScene);
    }

    //LOAD THE LEVEL FOR THE FIRST TIME OR RESTART
    public void PlayGame()
    {
            loadLevel = gameLevelsCount; //the level from the array as set in the Level_Select_Manager
            SceneManager.LoadScene(gameLevels[loadLevel]); //load first game level

            gameState = gameStates.Playing; //set the game state to playing

            endMsg = defaultEndMessage; //set the end message default

            playerWon = false; //set player winning condition to false  
    }//end StartGame()


    //EXIT THE GAME
    public void ExitGame()
    {
        SceneManager.LoadScene(startScene); //load the game over scene
        Debug.Log("Exited Game");
    }//end ExitGame()

    //Go to Settings Scene
    public void CreditsScene()
    {
        SceneManager.LoadScene(creditsScene); //load the game over scene
    }//end ExitGame()

    //GO TO THE GAME OVER SCENE
    public void GameOver()
    {
        gameState = gameStates.GameOver; //set the game state to gameOver

        if (playerWon) { endMsg = winMessage; } else { endMsg = loseMessage; } //set the end message

        SceneManager.LoadScene(gameOverScene); //load the game over scene
        Debug.Log("Game Over");
    }


    //GO TO THE NEXT LEVEL
    void NextLevel()
    {
        nextLevel = false; //reset the next level
        //as long as our level count is not more than the amount of levels
        if (gameLevelsCount < gameLevels.Length)
        {
            gameLevelsCount++; //add to level count for next level
            loadLevel = gameLevelsCount - 1; //find the next level in the array
            SceneManager.LoadScene(gameLevels[loadLevel]); //load next level
        }
        else
        { //if we have run out of levels go to game over
            GameOver();
        } //end if (gameLevelsCount <=  gameLevels.Length)
    }//end NextLevel()
}