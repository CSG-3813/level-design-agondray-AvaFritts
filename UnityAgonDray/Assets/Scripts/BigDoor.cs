using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    public string playerTag;
    private Animator animator;
    public string boolName;
    public string triggerName;
    public bool autoClose;
     
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();   
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
               AnimateDoor();
            }

        }
    }


    private void OnTriggerExit(Collider other)
    {
        bool testBool = animator.GetBool(boolName);

        if (autoClose && testBool)
        {
            GameObject ourObject = other.gameObject;
            if (ourObject.tag.CompareTo(playerTag).Equals(0))
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
