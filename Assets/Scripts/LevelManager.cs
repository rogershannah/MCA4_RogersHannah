using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float scoreValue = 1.0f;
    public int goalPickups = 1;
    public Text gameText;
    public Text scoreText;
    public Text pickupText;
    public Transform player;
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;

    public static bool isGameOver = false;

    public string nextLevel;

    float score = 0f;
    int pickupsCollected = 0;
    void Start()
    {
        isGameOver = false;
        SetPickupText();

        if (player == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdatePickups()
    {
        pickupsCollected++;
        if(pickupsCollected == goalPickups)
        {
            LevelBeat();
        }
        SetPickupText();
    }
    void SetPickupText()
    {
        pickupText.text = "Potions: " + pickupsCollected + "/" + goalPickups;
    }

    public void UpdateScore()
    {
        score += scoreValue;
        SetScoreText();
    }

    void SetScoreText()
    {
        scoreText.text = score.ToString() + " Slain";
    }

    public void LevelLost()
    {
        isGameOver = true;
        if(gameText != null)
        {
            gameText.text = "GAME OVER!";
            gameText.gameObject.SetActive(true);
        }

        AudioSource.PlayClipAtPoint(gameOverSFX, player.position);

        Invoke("LoadCurrentLevel", 2);
    }

    void LevelBeat()
    {
        isGameOver = true;
        if (gameText != null)
        {
            gameText.text = "YOU WIN!";
            gameText.gameObject.SetActive(true);
        }

        AudioSource.PlayClipAtPoint(gameWonSFX, player.position);

        if(!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
