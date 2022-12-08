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
    public bool autoClose;
    public bool isLocked;
     
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();   
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen && !isLocked)
        {
            AnimateDoor();
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
        bool testBool = animator.GetBool(boolName);

        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            canOpen = false;

            if (autoClose && testBool)
            {
                AnimateDoor();
            }
        }
    }

    public void ChangeOpenStatus(int num)
    {
        animator.SetBool("IsOpen", 0 >= num);
    }

    public void AnimateDoor()
    {
        animator.SetTrigger(triggerName);
    }

}
