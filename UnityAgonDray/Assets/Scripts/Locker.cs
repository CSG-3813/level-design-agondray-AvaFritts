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
    public string correctOrder;
    public SubmitLocker submitLocker;
    private Animator doorCtrl;
    public string boolName;

    private AudioSource door;
    public AudioClip openDoor;
    // Start is called before the first frame update
    void Awake()
    {
        doorCtrl = this.GetComponent<Animator>();
        door = this.GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                doorCtrl.SetBool(boolName, true);
                submitLocker.UpdateCode(correctOrder);
                door.PlayOneShot(openDoor);
            }
            
        }
    }

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
