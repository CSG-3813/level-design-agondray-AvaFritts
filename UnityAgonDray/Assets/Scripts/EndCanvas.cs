// Creator: Ava Fritts
//Date Created: May 10th 2022

// Last edited: May 10th 2022
// Description: The UI manager for the Game Over canvas.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndCanvas : MonoBehaviour
{
    public TMP_Text endingText;

    void Start()
    {
        endingText.text = GameManager.GM.endMsg;
    }

    public void RetryGame()
    {
        GameManager.GM.PlayGame();
    }

    public void ExitGame()
    {
        GameManager.GM.ExitGame();
    }

    public void StartingGame()
    {
        GameManager.GM.StartGame(); //go to Main Menu
    }
}
