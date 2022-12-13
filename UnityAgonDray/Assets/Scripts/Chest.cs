using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public string playerTag;

    [Space(10)]
    private Animator animator;
    public string triggerName;

    [Space(10)]
    public Collider trapppedObjectCollider;
    //public Pickup trapppedObject;
    public Collider thisTrigger;

    public bool canOpen = false;
    public bool isOpen = false;
    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen && !isOpen) //&& !isLocked)
        {
            AnimateChest();
            thisTrigger.enabled = false;

            if (trapppedObjectCollider != null)
            {
                trapppedObjectCollider.GetComponent<Collider>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToString().Equals(playerTag) && !isOpen)
        {
            canOpen = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.ToString().Equals(playerTag))
        {
            canOpen = false;
        }

    }

    public void AnimateChest()
    {
        animator.SetTrigger(triggerName);

    }
}
