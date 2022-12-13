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

public class Pickup : MonoBehaviour
{
    /*** VARIABLES ***/
    [Header("Set in Inspector")]
    [Tooltip("The corresponding value in the inventory Manager Script")]
    public int value;
    public string playerTag;
    [Space(10)]

    public GameObject itemHere;
    public string itemName;
    [Tooltip("Object it is trapped in if it is in a box")]
    public GameObject casing;

    [Space(10)]

    [Header("Set Dynamically")]
    public InventoryManager referenceItem; //the canvas it belongs to
    public bool canGet;
    public bool trappedInBox;

    // Start is called before the first frame update
    void Awake()
    {
        canGet = false;
        referenceItem = GameObject.FindObjectOfType<InventoryManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canGet)
        {
            AddToCanvas();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToString().Equals(playerTag))
        {
            print("Player found this " + itemName); 
            canGet = true;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.ToString().Equals(playerTag))
        {
            print("Mouse left this " + itemName);
            canGet = false;
        }
    }
    void AddToCanvas()
    {
        if (canGet == true)
        {
            //InventoryCanvas.current.Add(referenceItem);
            referenceItem.AddToInventory(value); //checks value
            itemHere.SetActive(false);

        }
    }
}
