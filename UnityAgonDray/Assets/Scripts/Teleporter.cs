using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    //VARIABLES 
    public bool canUse = false; //if the player isn't in range, it "pauses" the effects

    [Header("Movement Vectors: Set in Inspector")]
    public GameObject newLocation;
    //public float yPos;
    //public Vector3 cameraPositionOffset;

    public GameObject playerGO; 

    // Update is called once per frame
    void Update()
    {
        if (canUse)
        {
            if (Input.GetKeyDown(KeyCode.E)) //if the player interacts with it.
            {
                Debug.Log("Teleporting player");
                playerGO.transform.position = new Vector3(newLocation.transform.position.x, newLocation.transform.position.y, newLocation.transform.position.z);
                //Camera.main.transform.position += cameraPositionOffset; //Moves the camera.
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            canUse = true;
        }
    }//end OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        GameObject colGO = other.gameObject;
        if (colGO.tag.Equals("Player"))
        {
            canUse = false;
        }
    } //end OnTriggerExit
}
