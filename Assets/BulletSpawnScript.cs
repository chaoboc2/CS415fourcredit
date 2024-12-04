using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{
    public GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)== true)
        {
            SpawnBullet();
        }
    }


    void SpawnBullet()
    {
        GameObject newPipe = Instantiate(bullet, transform.position, transform.rotation);
    }
}
