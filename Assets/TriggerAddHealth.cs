using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddHealth : MonoBehaviour
{
    public LogicScript logic;
    public int healthToAdd = 1;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Console.WriteLine("health!");
            logic.AddHealth(healthToAdd);
            Destroy(gameObject);  // Destroy the health pack after collection
        }

    }
}
