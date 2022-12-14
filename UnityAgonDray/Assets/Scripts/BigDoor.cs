//UPTDATED DEC 13 BY AVA FRITTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    public string playerTag;
    private Animator animator;
    public string boolName;
    public string triggerName;

    [Space(10)]
    public bool canOpen;
    public bool isOpen = false;
    public bool autoClose;

    [Space(10)]
    public bool isLocked;
    public bool needsKey;
    [Tooltip("The object representing the lock. Set it active if it needs to disappear and hide it if it needs to be placed")]
    public GameObject lockedObject;
    public InventoryManager inventoryManager;
    [Tooltip("Set to the ID of the corresponding Key in the inventory")]
    public int keyIDTag;
     
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        if (needsKey)
        {
            isLocked = true;
            inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        }
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen && !isLocked)
        {
            AnimateDoor();
        } else if (Input.GetKeyDown(KeyCode.E) && canOpen && isLocked)
        {
            if (needsKey)
            {
                CheckForKey();
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            canOpen = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //bool testBool = animator.GetBool(boolName);

        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            canOpen = false;

            if (autoClose && isOpen)
            {
                AnimateDoor();
            }
        }
    }
    public void CheckForKey()
    {
        if(inventoryManager != null)//need one for this!
        {
            if(inventoryManager.idTag.CompareTo(keyIDTag) == 0) //making sure the item is selected
            {
             //if the item is active and usable
                if (inventoryManager.UseItem(keyIDTag))
                {
                    isLocked = false;
                    SwapLock();
                    AnimateDoor();
                }
            }
        }
    }

    public void SwapLock()
    {
        if (lockedObject.activeInHierarchy)
        {
            lockedObject.SetActive(false);
        } else
        {
            lockedObject.SetActive(true);
        }
    }

    /**public void ChangeOpenStatus(int num)
    {
        animator.SetBool("IsOpen", 0 >= num);
    }**/

    public void DoorOpen()
    {
        isOpen = true;
        animator.SetBool(boolName, true);
    }

    public void DoorClose()
    {
        isOpen = false;
        animator.SetBool(boolName, false);
    }

    public void AnimateDoor()
    {
        animator.SetTrigger(triggerName);
        if (isOpen)
        {
           DoorClose();
        }
        else
        {
            DoorOpen();
        }
    }

}
