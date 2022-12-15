using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPlay : MonoBehaviour
{
    public PuzzleScript puzzleScript;

    [Space(10)]
    public bool isInRange;
    public string playerTag;
    //public GameObject lightGO;

    private void Update()
    {
        if(isInRange)
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Trigger start");
            puzzleScript.TriggerStart();
        }

        /**if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Reset clear status");
            puzzleScript.ResetClearStatus();
        }**/
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            isInRange = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject ourObject = other.gameObject;
        if (ourObject.tag.CompareTo(playerTag).Equals(0))
        {
            isInRange = false;
        }
    }

}
