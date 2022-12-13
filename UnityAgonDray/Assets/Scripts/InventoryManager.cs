/**** 
 * Created by: Ava Fritts
 * Date Created: Feb 26, 2022
 * 
 * Last Edited: Dec 12, 2022
 * 
 * Description: Script for the inventory system.
****/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    /** Variables**/
    [Header("Set in Inspector")]
    public GameObject[] numItemsArray; //The array of inventory Buttons
    public UsableItems[] usableItemsArray;
    [Tooltip("This item moves to show what item is selected.")]
    public GameObject selectionCursor;

    [Header("Edit dynamically")]
    public int numItemsIDList;

    [Space(10)]
    [Tooltip("The ID of the current item Selected. First item is numbered zero. Set to -1 if you aren't using an item")]
    public int idTag;
    public string currentItem;

    private void Awake()
    {
        //deactivate every single inventory button
        foreach (GameObject ivButton in numItemsArray)
        {
            numItemsIDList++;
            ivButton.SetActive(false);
        }
        //selectionCursor.GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectItem(0);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectItem(1);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectItem(2);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectItem(3);
        } /**else if (Input.GetKeyDown(KeyCode.E))
        {
            UseItem(idTag);
        }**/

    }

    public void SelectItem(int itemCode)
    {
        if (!selectionCursor.activeInHierarchy) { selectionCursor.SetActive(true); }

        if(idTag >= 0)
        {
            usableItemsArray[idTag].DeselectItem();
        }

        switch (itemCode) 
        {
            case 0:
                idTag = 0;
                selectionCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(selectionCursor.transform.position.x, -66);
                usableItemsArray[0].SwapToItem();
                break;
            
            case 1:
                idTag = 1;
                selectionCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(selectionCursor.transform.position.x, -146);
                usableItemsArray[1].SwapToItem();
                break;
            
            case 2:
                idTag = 2;
                selectionCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(selectionCursor.transform.position.x, -226);
                usableItemsArray[2].SwapToItem();
                break;
            
            case 3:
                idTag = 3;
                selectionCursor.GetComponent<RectTransform>().anchoredPosition = new Vector2(selectionCursor.transform.position.x, -306);
                usableItemsArray[3].SwapToItem();
                break;
                
            default: 
                selectionCursor.SetActive(false);
                break;  
        }

    }

    //this method UNDER NO CIRCUMSTANCES should be accessed if it is Negative one
    public bool UseItem(int idNumber)
    {
        return usableItemsArray[idTag].UseItem();
    }
    public void RemoveItem(int currItem)
    {
        //transform.tag = "Untagged"; //prevents objects from being used multiple times

        for (int i = 0; i < numItemsIDList; i++)
        {
            if (currItem == i) //if the array finds the correct number
            {
                numItemsArray[i].SetActive(false);//deactivate the button I need
                return;
            }
        }
    }

    public void AddToInventory(int testValue)
    {
        for (int i = 0; i < numItemsIDList; i++)
        {
            if (testValue == i) //if the array finds the correct number
            {
                numItemsArray[i].SetActive(true);//activate the button I need
            }
        }
    }

}

