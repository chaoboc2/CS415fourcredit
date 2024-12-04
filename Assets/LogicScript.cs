using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public int playerHealth;
    public Text scoreText;
    public Text healthText;
    public GameObject gameOverScreen;

    public int shieldHold;
    public Text shieldText;

    public GameObject heartPrefab;    
    public Transform heartsParent;
    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        // Initialize hearts
        Time.timeScale = 1f;  // Ensure normal time when scene starts
        UpdateHeartDisplay();

        shieldHold = 2;
        UpdateShieldDisplay();
    }

    void UpdateShieldDisplay()
    {
        shieldText.text = "Shield: " + shieldHold.ToString();
    }

    public void AddShield(int amount)
    {
        shieldHold += amount;
        UpdateShieldDisplay();
    }

    public bool UseShield()
    {
        if (shieldHold > 0)
        {
            shieldHold--;
            UpdateShieldDisplay();
            return true;
        }
        return false;
    }

    public void addScore( int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = "Score: " + playerScore.ToString();
    }

    void UpdateHeartDisplay()
    {
        // Clear existing hearts
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        // Create new hearts based on current health
        for (int i = 0; i < playerHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsParent);
            hearts.Add(heart);
        }
    }

    public int decreasehealth(int healthToAdd)
    {
        playerHealth = playerHealth - healthToAdd;
        //healthText.text = "Health: " + playerHealth.ToString();
        UpdateHeartDisplay();

        return playerHealth;
    }

    public void AddHealth(int amount)
    {
        playerHealth = Mathf.Min(playerHealth + amount, 5);  // Assuming max health is 5
        UpdateHeartDisplay();  // Update the heart UI
    }

    public void restartGame()
    {
        Time.timeScale = 1f;  // Restore normal time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        Time.timeScale = 0f;  // Freeze the game
        gameOverScreen.SetActive(true);
    }
}
