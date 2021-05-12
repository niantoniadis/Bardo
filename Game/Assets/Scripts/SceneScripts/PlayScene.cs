using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScene : MonoBehaviour
{
    // ui elements
    public GameObject MenuPanel;
    public GameObject HideButton;
    public GameObject InstructionsText;
    public GameObject InstructionsButton;
    public GameObject ResumeButton;
    public GameObject MainMenuButton;
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public Player player;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (InstructionsText.activeSelf)
            {
                HideInstructions();
            }
            else if (MenuPanel.activeSelf)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
        if (!MenuPanel.activeSelf && FloorManager.gameStarted)
        {
            if (player.currentHealth <= 0)
            {
                LoseGame();
            }
            else if (player.room.PlayerWon())
            {
                WinGame();
            }
        }
        if (Input.anyKeyDown)
        {
            if (WinScreen.activeSelf || LoseScreen.activeSelf)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    /// <summary>
    /// Pauses the game
    /// </summary>
    public void PauseGame()
    {
        MenuPanel.SetActive(true);

        // pause code here
        Time.timeScale = 0f;
        player.room.active = false;
        player.GetComponent<PlayerMovement>().isPaused = true;
        player.isPaused = true;
        for (int i = 0; i < player.room.Enemies.Count; i++)
        {
            player.room.Enemies[i].isPaused = true;
        }
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        player.room.active = false;
        player.GetComponent<PlayerMovement>().isPaused = true;
        for (int i = 0; i < player.room.Enemies.Count; i++)
        {
            player.room.Enemies[i].isPaused = true;
        }

        LoseScreen.SetActive(true);
    }

    /// <summary>
    /// Sends the player back to the title screen
    /// </summary>
    public void ToTitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// Puts the player back in the game
    /// </summary>
    public void Resume()
    {
        MenuPanel.SetActive(false);

        // resume code here
        Time.timeScale = 1f;
        player.room.active = true;
        player.GetComponent<PlayerMovement>().isPaused = false;
        player.isPaused = false;
        for (int i = 0; i < player.room.Enemies.Count; i++)
        {
            player.room.Enemies[i].isPaused = false;
        }
    }

    /// <summary>
    /// Shows the player the instructions again
    /// </summary>
    public void Instructions()
    {
        InstructionsText.SetActive(true);
        HideButton.SetActive(true);
        ResumeButton.SetActive(false);
        MainMenuButton.SetActive(false);
        InstructionsButton.SetActive(false);
    }

    /// <summary>
    /// hides the instructions again
    /// </summary>
    public void HideInstructions()
    {
        InstructionsText.SetActive(false);
        HideButton.SetActive(false);
        ResumeButton.SetActive(true);
        MainMenuButton.SetActive(true);
        InstructionsButton.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        player.room.active = false;
        player.GetComponent<PlayerMovement>().isPaused = true;
        for (int i = 0; i < player.room.Enemies.Count; i++)
        {
            player.room.Enemies[i].isPaused = true;
        }

        WinScreen.SetActive(true);
    }
}
