/**** 
 * Created by: Ava Fritts
 * Date Created: Feb 18, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 2, 2022
 * 
 * Description: This script manages the pickup and management of items.
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableItems : MonoBehaviour
{

    /*** VARIABLES ***/
    [Header("Set in Inspector")]
    public int value;
 
    [Space(10)]

    public GameObject itemIcon;
    public string itemName;
    [Space(10)]

    [Tooltip("Will the item go away after use?")]
    public bool canBreak;

    [Header("SetDynamically")]
    [Tooltip("ONLY SET IF CAN BREAK IS TRUE")]
    public int numUses = 1; //how many times the item can be used
    [Tooltip("Sometimes this will need to be active, like if you get an item that carries over between levels")]
    public bool canUse = false;


   /** private void Awake()
    {
        
    }**/

    public bool UseItem()
    {
        if (canUse)
        {
            if (canBreak)
            {
                numUses -= 1;
                if (numUses <= 0)
                {
                    Break();
                }
            }
            return true;
        }
        return false;
    }

    public void SwapToItem()
    {
        if (itemIcon.activeInHierarchy)
        {
            canUse = true;
        }
    }

    public void DeselectItem()
    {
        canUse = false;
    }

    private void Break()
    {
        canUse = false;
        itemIcon.SetActive(false);
    }

}
