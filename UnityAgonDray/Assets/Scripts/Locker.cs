/*
 * Made By: Ava Fritts
 * Date Made: 11-27-2022
 * Date Edited:
 * Description: The Locker Controller for the Puzzle. The submit button has a different script.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;

public class Locker : MonoBehaviour
{
    //VARS
    public string playerTag;
    bool canOpen = false;
    public string correctOrder;
    public SubmitLocker submitLocker;
    private Animator doorCtrl;
    public string boolName;
    public bool hasUpdated;

    private AudioSource door;
    public AudioClip openDoor;
    // Start is called before the first frame update
    void Awake()
    {
        doorCtrl = this.GetComponent<Animator>();
        door = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorCtrl.SetBool(boolName, true);
                if (!hasUpdated)
                {
                    submitLocker.UpdateCode(correctOrder);
                    hasUpdated = true;
                }
                
                door.PlayOneShot(openDoor);
            }
        }
           
            
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            canOpen = true;
        }
    }//end OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            canOpen = false;
        }
    } //end OnTriggerExit

    public void ResetLocker()
    {
        doorCtrl.SetBool(boolName, false);
    }

   /* private void OnTriggerExit(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {

        }
    }*/
}
