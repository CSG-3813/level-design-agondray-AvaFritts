using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public string playerTag;
    private Animator animator;
    //private AudioSource audioSource;
    public bool hasCollected = false;
    public bool canCollect = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        //audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canCollect)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Collect");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            animator.SetBool("InArea", true);
            canCollect = true;
        }
    }

    void CollectSword() 
    {
        hasCollected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (!hasCollected)
            {
                animator.SetBool("InArea", false);
                canCollect = false;
            }
        }
    }
}
