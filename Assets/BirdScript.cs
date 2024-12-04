using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRidgidbody;
    public float flapStrength;
    public LogicScript logic;
    public int playerHealth;

    private bool isInvincible = false;
    //private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] allSprites;  // Change from single spriteRenderer
    private SpriteRenderer[] birdSprites;  // All sprite renderers except shield

    private Collider2D birdCollider;
    private float invincibilityDuration = 3;
    private float flickerInterval = 0.1f;

    private int originalLayer;

    private bool isShieldActive = false;
    public float shieldDuration = 3f;
    public GameObject shieldVisual; // Assign a sprite or effect for the shield

    public Animator animator;
    public bool flapped = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        //spriteRenderer = GetComponent<SpriteRenderer>();
        allSprites = GetComponentsInChildren<SpriteRenderer>();
        birdSprites = allSprites.Where(sprite =>
        sprite.gameObject != shieldVisual &&
        !sprite.transform.IsChildOf(shieldVisual.transform))
        .ToArray();// Gets all sprite renderers

        birdCollider = GetComponent<Collider2D>();

        originalLayer = gameObject.layer;  // Store original layer
    }

    // Update is called once per frame
    void Update()
    {
        //fly up
        if (Input.GetKeyDown(KeyCode.Space)==true)
        {
            flapped = true;
            animator.SetBool("IsFlap", flapped);
            myRidgidbody.velocity = Vector2.up * flapStrength;
        }
        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            flapped = false;
            animator.SetBool("IsFlap", flapped);
        }

            if (Input.GetKeyDown(KeyCode.E) && !isInvincible && !isShieldActive)
        {
            ActivateShield();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible && !isShieldActive)
        {
            Debug.Log("bump");
            StartCoroutine(HandlePipeCollision());
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("border");
            decreaseHealthToZero();
        }
    }

    void ActivateShield()
    {
        if (logic.UseShield()) // Only activate if we have shield charges
        {
            StartCoroutine(HandleShieldEffect());
        }
    }

    IEnumerator HandleShieldEffect()
    {
        isShieldActive = true;
        isInvincible = true;
        gameObject.layer = LayerMask.NameToLayer("InvincibleBird");

        // Enable shield visual
        shieldVisual.SetActive(true);
        SpriteRenderer shieldRenderer = shieldVisual.GetComponent<SpriteRenderer>();

        // Flicker the shield while keeping bird visible
        float elapsedTime = 0f;
        while (elapsedTime < shieldDuration)
        {
            shieldRenderer.enabled = !shieldRenderer.enabled;
            yield return new WaitForSeconds(flickerInterval);
            elapsedTime += flickerInterval;
        }

        // Restore original state
        shieldVisual.SetActive(false);
        gameObject.layer = originalLayer;
        isShieldActive = false;
        isInvincible = false;
    }

    IEnumerator HandlePipeCollision()
    {
        // Enable invincibility
        isInvincible = true;
        //birdCollider.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("InvincibleBird");  // Change to invincible layer instead of disabling collider

        // Decrease health
        decreaseHealth();

        // Start flickering effect
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            //spriteRenderer.enabled = !spriteRenderer.enabled;
            // Toggle all sprites
            foreach (SpriteRenderer sprite in birdSprites)
            {
                sprite.enabled = !sprite.enabled;
            }
            yield return new WaitForSeconds(flickerInterval);
            elapsedTime += flickerInterval;
        }

        // Restore original state
        //spriteRenderer.enabled = true;
        // Restore all sprites to visible
        foreach (SpriteRenderer sprite in birdSprites)
        {
            sprite.enabled = true;
        }
        //birdCollider.enabled = true;
        gameObject.layer = originalLayer;  // Restore original layer
        isInvincible = false;
    }

    void decreaseHealth()
    {
        int playerHealth=logic.decreasehealth(1);
        if (playerHealth <= 0)
        {
            logic.gameOver();
        }
    }

    void decreaseHealthToZero()
    {
        int playerHealth = logic.decreasehealth(5);
        if (playerHealth <= 0)
        {
            logic.gameOver();
        }
    }
}
