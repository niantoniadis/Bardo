using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    // ui elements
    public GameObject instructions;
    public GameObject instructionsButton;
    public GameObject startButton;

    // disables the start button and enables the instruction text
    public void ToInstructions()
    {
        instructionsButton.SetActive(false);
        startButton.gameObject.SetActive(true);
        instructions.SetActive(true);
    }

    // loads the game scene
    public void ToPlayScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

