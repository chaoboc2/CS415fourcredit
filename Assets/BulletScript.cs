using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 10f;

    void Update()
    {
        // Move bullet to the right
        transform.position += Vector3.right * bulletSpeed * Time.deltaTime;

        // Optional: Destroy bullet if it goes too far off screen
        if (transform.position.x > 25)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Don't destroy if hitting the bird
        if (collision.gameObject.layer != 3 && collision.gameObject.layer != 6)  // Assuming bird has "Player" tag
        {
            // Destroy the parent of collided object (which will destroy all children)
            if (collision.gameObject.transform.parent != null)
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
            // If no parent, destroy the object itself
            else
            {
                Destroy(collision.gameObject);
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
