// Creator: Ava Fritts
// Date Created: May 10th 2022

// Last edited: Dec 10th 2022
// Description: The script for the starting scene.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{ 
    // public Text titleText;

    /** void Start()
     {
         //titleText.text = GameManager.GM.gameTitle;
     }**/

    public void LoadCredits()
    {
        GameManager.GM.CreditsScene();
    }

    public void StartingGame()
    {
        GameManager.GM.StartGame();
    }
}