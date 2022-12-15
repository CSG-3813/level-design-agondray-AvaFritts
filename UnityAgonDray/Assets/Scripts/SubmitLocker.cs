using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitLocker : MonoBehaviour
{
    public string playerTag;
    public string correctAnswer;
    private Animator doorCtrl;
    public string boolName;

    [Space(10)]

    public Collider trapppedObjectCollider;
    public Collider thisTrigger;

    [Space(10)]

    private AudioSource door;
    public AudioClip openDoor;
    public AudioClip closeDoor;
    public Locker[] lockerList;

    public string currentNumber;

    [Space(10)]
    [Header("Big Door the Locker Unlocks")]
    public Animator animator;
    public string triggerName;

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
                CheckCode(currentNumber);
               
            }

        }
    }

    public void UpdateCode(string number)
    {
        currentNumber += number;
        Debug.Log("Current Number = "+ currentNumber);
    }

    public void CheckCode(string submittedAnswer)
    {
           
        if (submittedAnswer.CompareTo(correctAnswer) != 0)       
        {             
            currentNumber = "";    
            foreach (Locker hlep in lockerList)       
            {        
                hlep.ResetLocker();      
                hlep.hasUpdated = false;  
            }     
            door.PlayOneShot(closeDoor);   
            return;   
        }

        doorCtrl.SetBool(boolName, true);
        door.PlayOneShot(openDoor);

        //Open the Door
        animator.SetTrigger(triggerName);

        thisTrigger.enabled = false;
        trapppedObjectCollider.GetComponent<Collider>().enabled = true;
    }

}
