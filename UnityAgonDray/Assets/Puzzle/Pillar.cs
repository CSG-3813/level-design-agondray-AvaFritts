using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public Coords coords;
    public Direction dir = Direction.FORWARD;
    public GameObject arrowGO;
    public MeshRenderer meshRenderer;
    [Tooltip("Red, Yellow, Green, Purple, Black IN THAT ORDER")]
    public Material[] materials;
    bool canRotate = false;

    private void Awake()
    {
        //meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canRotate)
        {
            RotateTile();
        }
    }

    private void Start()
    {
        UpdateVisuals();
    }

    public void RotateTile()
    {
        switch (dir)
        {
            case Direction.FORWARD:
                dir = Direction.RIGHT;
                break;
            case Direction.RIGHT:
                dir = Direction.BACK;
                break;
            case Direction.BACK:
                dir = Direction.LEFT;
                break;
            case Direction.LEFT:
                dir = Direction.FORWARD;
                break;
            case Direction.FINISH:
                break;
        }
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        switch(dir)
        {
            //ALTER THIS TO MAKE ARROWS, NOT CHANGE COLORS

            case Direction.FORWARD:
                //Arrow in positive Z direction
                arrowGO.transform.eulerAngles = new Vector3 (180, 0, 0);
                meshRenderer.material = materials[0];
                break;
            case Direction.BACK:
                //Arrow in negative Z direction
                arrowGO.transform.eulerAngles = new Vector3(180, -66, 0);
                meshRenderer.material = materials[1];
                break;
            case Direction.LEFT:
                //Arrow in negative X direction
                arrowGO.transform.eulerAngles = new Vector3(180, 33, 0);
                meshRenderer.material = materials[2];
                break;
            case Direction.RIGHT:
                //Arrow in positive X direction
                arrowGO.transform.eulerAngles = new Vector3(180, -33, 0);
                meshRenderer.material = materials[3];
                break;
            case Direction.FINISH:
                meshRenderer.material = materials[4];
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canRotate = false;
        }
    }
}

public enum Direction
{
    FORWARD, BACK, LEFT, RIGHT, FINISH
}