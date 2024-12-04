using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoving : MonoBehaviour
{
    public float verticalSpeed = 2f;  // Speed of up/down movement
    public float moveDistance = 2f;   // How far up and down it moves
    
    private float startingY;          // Starting Y position
    private float time = 0f;          // Time tracker for smooth movement
    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Add sine wave movement for smooth up/down
        time += Time.deltaTime;
        float newY = startingY + Mathf.Sin(time * verticalSpeed) * moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
}
