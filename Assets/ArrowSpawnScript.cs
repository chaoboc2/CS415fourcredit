using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawnScript : MonoBehaviour
{
    public GameObject[] pipes;
    public float spawnRate = 2;

    public float minSpawnRate = 1f;  // Fastest spawn rate allowed
    public float spawnRateDecrease = 0.05f;  // How much to decrease spawn rate per interval
    private float difficultyTimer = 0;
    public float difficultyInterval = 10;  // Increase difficulty every 10 seconds

    // Reference to pipe movement script to update speed
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 10f;  // Maximum movement speed
    public float speedIncrease = 0.2f;  // How much to increase speed per interval

    private float timer = 0;
    public float heightOffset = 10;

    private float myRandomBool;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Regular spawn timer
        timer += Time.deltaTime;
        if (timer > spawnRate)
        {
            timer = 0;
            //random decide 
            myRandomBool = Random.Range(0, 20);
            //Debug.Log(myRandomBool);
            if (myRandomBool >= 15)
            {
                SpawnPipe();
            }
            
        }

        // Difficulty increase timer
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer > difficultyInterval)
        {
            difficultyTimer = 0;
            IncreaseDifficulty();
        }
    }

    void IncreaseDifficulty()
    {
        // Increase movement speed
        if (moveSpeed < maxMoveSpeed)
        {
            moveSpeed = Mathf.Min(moveSpeed + speedIncrease, maxMoveSpeed);
        }

        // Decrease spawn rate (make pipes spawn faster)
        if (spawnRate > minSpawnRate)
        {
            spawnRate = Mathf.Max(spawnRate - spawnRateDecrease, minSpawnRate);
        }
    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        int randomIndex = Random.Range(0, pipes.Length);
        GameObject newPipe = Instantiate(pipes[randomIndex], new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

        // Set the current move speed on the new pipe
        PipeMoveScript pipeMove = newPipe.GetComponent<PipeMoveScript>();
        if (pipeMove != null)
        {
            pipeMove.moveSpeed = moveSpeed;
        }
    }
}
