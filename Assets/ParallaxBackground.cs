using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float backgroundWidth = 50f;  // Width of your background sprite
    public float parallaxEffect = 1f;

    private void Update()
    {
        float moveAmount = scrollSpeed * parallaxEffect * Time.deltaTime;
        transform.Translate(Vector2.left * moveAmount);

        //another multiply by 2 for the scale of backgroundmanager
        if (transform.position.x <= -backgroundWidth*2-18)
        {
            Vector3 position = transform.position;
            //another multiply by 2 for the scale of backgroundmanager
            position.x += backgroundWidth * 2*2;  // Move it two widths over
            transform.position = position;
        }
    }
}
