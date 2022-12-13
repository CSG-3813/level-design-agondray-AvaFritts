using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    /*** VARIABLES ***/
    [Header("Set in Inspector")]
    [Tooltip("The corresponding value in the inventory Manager Script")]
    public int itemValue;
    public string playerTag;

    [Space(10)]

    public GameObject itemHere;
    public string itemName;

    [Space(10)]
    [Header("Set Dynamically")]
    public InventoryManager referenceItem; //the canvas it belongs to
    private Animator animator;
    //private AudioSource audioSource;
    public bool hasCollected = false;
    public bool canCollect = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        referenceItem = GameObject.FindObjectOfType<InventoryManager>();
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
        referenceItem.AddToInventory(itemValue); //checks value
        itemHere.SetActive(false);
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
