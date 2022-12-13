//MADE AND UPDATED DEC 13 BY AVA FRITTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public string playerTag;
    public bool canUse = false; //if the player isn't in range, it "pauses" the effects
    public InventoryManager inventoryManager;
    [Tooltip("Set to the ID of the corresponding Key in the inventory")]
    public int keyIDTag;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUse)
        {
            CheckForItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            canUse = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            canUse = false;     
        }
    }
    public void CheckForItem()
    {
        if (inventoryManager != null)//need one for this!
        {
            if (inventoryManager.idTag.CompareTo(keyIDTag) == 0) //making sure the item is selected
            {
                //if the item is active and usable
                if (inventoryManager.UseItem(keyIDTag))
                {
                 //Beat the level
                 GameManager.GM.nextLevel = true;
                }
            }
        }
    }

}
