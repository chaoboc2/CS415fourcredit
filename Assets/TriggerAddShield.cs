using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddShield : MonoBehaviour
{
    public LogicScript logic;
    public int shieldToAdd = 1;
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
            logic.AddShield(shieldToAdd);
            Destroy(gameObject);
        }
    }
}
