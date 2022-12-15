using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    public GameObject Pillar;
    public GameObject ball;
    [Tooltip("Used for visual indication of (1, 1). Will be destroyed on play.")] public GameObject ExamplePillar;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public int rows;
    public int cols;
    public float durationForMove;
    public float distanceBetweenTiles = 1f;
    public float ballY = .5f;
    public string wallColliderTag;
    public bool randomizesOnFailure = false;
    public bool printsFailureMessages = false;

    PuzzleState state;
    Direction currDir = Direction.FORWARD;
    Coords currCoords;
    float timer = 0f;
    bool[,] positionChecked;
    GameObject[,] pillars;

    // Start is called before the first frame update
    void Start()
    {
        state = PuzzleState.Idle;
        ExamplePillar.SetActive(false);
        positionChecked = new bool[rows, cols];
        pillars = new GameObject[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                positionChecked[i, j] = false;

                //Should probably do object pooling here iirc
                #region Pillar instantiation
                GameObject localPillar = Instantiate(Pillar);
                pillars[i, j] = localPillar;
                localPillar.GetComponentInChildren<Pillar>().coords = new Coords(i + 1, j + 1);
                localPillar.transform.position = new Vector3(transform.position.x + (i * distanceBetweenTiles), transform.position.y, transform.position.z + (j * distanceBetweenTiles));
                #endregion

                GiveRandomDirection(localPillar, i, j);
            }
        }

        SetStartingBallPosition();
    }

    #region Puzzle and Pillar Setup
    void StartPuzzle()
    {
        currCoords = new Coords((int)startPosition.x, (int)startPosition.y); //h
        SetStartingBallPosition();
        positionChecked[currCoords.x - 1, currCoords.y - 1] = true;
        SetDirection();
        state = PuzzleState.Playing;
    }

    void ResetPuzzle()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                positionChecked[i, j] = false;
            }
        }

        if (randomizesOnFailure)
        {
            GenerateGrid();
        }

        SetStartingBallPosition();
        state = PuzzleState.Idle;
    }

    void SetStartingBallPosition()
    {
        SetBallPosition(startPosition.x, startPosition.y);
    }

    void GenerateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                positionChecked[i, j] = false;
                GameObject localPillar = pillars[i, j];
                GiveRandomDirection(localPillar, i, j);
            }
        }
    }

    void GiveRandomDirection(GameObject pillar, int row, int col)
    {
        if (row == endPosition.x - 1 && col == endPosition.y - 1)
        {
            pillar.GetComponentInChildren<Pillar>().dir = Direction.FINISH;
        }
        else
        {
            int randomDirectionValue = Random.Range(0, 4);
            switch (randomDirectionValue)
            {
                case 0:
                    pillar.GetComponentInChildren<Pillar>().dir = Direction.FORWARD;
                    break;
                case 1:
                    pillar.GetComponentInChildren<Pillar>().dir = Direction.BACK;
                    break;
                case 2:
                    pillar.GetComponentInChildren<Pillar>().dir = Direction.LEFT;
                    break;
                case 3:
                    pillar.GetComponentInChildren<Pillar>().dir = Direction.RIGHT;
                    break;
                default:
                    break;
            }
        }

        pillar.GetComponentInChildren<Pillar>().UpdateVisuals();
    }

    void SetDirection()
    {
        Pillar currPillar = pillars[currCoords.x - 1, currCoords.y - 1].GetComponentInChildren<Pillar>();

        currDir = currPillar.dir;
    }

    #endregion

    void RunPuzzle()
    {
        Debug.Log(currDir);

        //Raycast in curr direction to check if hitting wall
        RaycastHit hit;
        if (Physics.Raycast(ball.transform.position, transform.TransformDirection(GetRaycastAngle()), out hit, distanceBetweenTiles))
        {
            //If hit is tagged with wall tag
            if (hit.collider.gameObject.CompareTag(wallColliderTag))
            {
                FailPuzzle("Hit invisible wall");
                return;
            }
        }

        //Check if trying to leave boundaries and move if not
        switch (currDir)
        {
            case Direction.FORWARD:

                //If stepping forward by 1 is out of grid
                if (currCoords.y + 1 > cols)
                {
                    FailPuzzle("Out of bounds at y = " + (currCoords.y + 1));
                    return;
                }
                currCoords.y += 1;
                SetBallPosition(currCoords.x, currCoords.y);
                break;

            case Direction.BACK:

                //If stepping back by 1 is out of grid
                if (currCoords.y - 1 < 1)
                {
                    FailPuzzle("Out of bounds at y = " + (currCoords.y - 1));
                    return;
                }
                currCoords.y -= 1;
                SetBallPosition(currCoords.x, currCoords.y);
                break;

            case Direction.LEFT:

                //If stepping back by 1 is out of grid
                if (currCoords.x - 1 < 1)
                {
                    FailPuzzle("Out of bounds at x = " + (currCoords.x - 1));
                    return;
                }
                currCoords.x -= 1;
                SetBallPosition(currCoords.x, currCoords.y);
                break;

            case Direction.RIGHT:

                //If stepping forward by 1 is out of grid
                if (currCoords.x + 1 > rows)
                {
                    FailPuzzle("Out of bounds at x = " + (currCoords.x + 1));
                    return;
                }
                currCoords.x += 1;
                SetBallPosition(currCoords.x, currCoords.y);
                break;

            default:
                break;
        }

        //Check if position has been visited before
        if (positionChecked[currCoords.x - 1, currCoords.y - 1] == true)
        {
            FailPuzzle("Backtracked");
            return;
        }

        //Mark current spot as checked
        positionChecked[currCoords.x - 1, currCoords.y - 1] = true;

        SetDirection();

        //Check if on end position
        if (currCoords.x == endPosition.x && currCoords.y == endPosition.y)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        Debug.Log("Puzzle completed");
        state = PuzzleState.Completed;

    }

    void FailPuzzle(string reason) {

        ResetPuzzle();

        if (printsFailureMessages)
        {
            Debug.Log("FAILURE: " + reason);
        }

    }

    // Used to trigger the beginning of the puzzle execution from another script
    public bool TriggerStart()
    {
        if (state == PuzzleState.Idle)
        {
            Debug.Log("Starting puzzle...");
            StartPuzzle();
            return true;
        }
        else
        {
            return false;
        }

    }

    public void ResetClearStatus()
    {
        if (state == PuzzleState.Completed)
        {
            ResetPuzzle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PuzzleState.Idle:
                IdleState();
                break;
            case PuzzleState.Playing:
                PlayingState();
                break;
            case PuzzleState.Completed:
                CompletedState();
                break;
        }
    }

    #region States
    void IdleState()
    {
        
    }

    void PlayingState()
    {
        Debug.DrawRay(ball.transform.position, transform.TransformDirection(GetRaycastAngle()) * distanceBetweenTiles, Color.red);
        timer += Time.deltaTime;
        if (timer > durationForMove)
        {
            timer = 0f;
            RunPuzzle();
        }
    }

    void CompletedState()
    {

    }

    #endregion

    #region Ball Logic

    void SetBallPosition(float x, float z)
    {
        //If visual, start a coroutine here to lerp the position over durationForMove time

        ball.transform.position = new Vector3(transform.position.x + ((int)(x - 1) * distanceBetweenTiles), transform.position.y + ballY, transform.position.z + ((int)(z - 1) * distanceBetweenTiles));
    }

    Vector3 GetRaycastAngle()
    {
        switch (currDir)
        {
            case Direction.FORWARD:
                //Rotate forward
                return Vector3.forward;
            case Direction.BACK:
                //Rotate back
                return Vector3.back;
            case Direction.LEFT:
                //Rotate left
                return Vector3.left;
            case Direction.RIGHT:
                return Vector3.right;
            default:
                Debug.LogWarning("Sitting on FINISH and it didn't register fully");
                return Vector3.zero;

        }
    }

    #endregion

}

public class Coords
{
    public int x;
    public int y;
    public Coords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public enum PuzzleState
{
    Idle, Playing, Completed
}