using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    Canvas startCanvas;
    Canvas gameOverCanvas;
    GameObject startGO;

    private void Start()
    {
        TutorialManager tutorial = FindObjectOfType<TutorialManager>();
        if (tutorial != null)
        {
            Destroy(tutorial.gameObject);
        }
        if (gameObject.tag == "WinUI")
        {
            unlockLevels();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("space")) {
            startGame();
        }
    }

    public void startGame()
    {
        startGO = new GameObject();
        SettingsManager settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsManager>();
        startCanvas = FindObjectOfType<Canvas>();
        Destroy(startCanvas.gameObject);
        startGO.AddComponent<MapGenerator>();
        Instantiate(Resources.Load<Canvas>("InGameUI"), new Vector3(0, 0, 0), Quaternion.identity);
        if (settings.getCurrentLevel() == 0)
        {
            Instantiate(Resources.Load<TutorialManager>("tutorial"), new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void Quit()
    {
        gameOverCanvas = FindObjectOfType<Canvas>();
        Destroy(gameOverCanvas.gameObject);
        Instantiate(Resources.Load<Canvas>("StartUI"), new Vector3(0, 0, 0), Quaternion.identity);
    }

    public static void addYourScore(int score)
    {
        GameObject yourScore = GameObject.FindGameObjectWithTag("YourScore");
        if (yourScore != null)
        {
            yourScore.GetComponent<TMP_Text>().text = "Your Score : " + score;
        }
        
    }

    public void displaySettings()
    {
        Destroy(gameObject.gameObject);
        Instantiate(Resources.Load<Canvas>("SettingsUI"), new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void unlockLevels()
    {
        SettingsManager settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsManager>();
        if (settings.getCurrentLevel() == 3)
        {
            return;
        }
        GameObject.FindGameObjectWithTag("Unlock").GetComponent<TMP_Text>().text = "You unlock level " + (settings.getCurrentLevel() + 2) + " in options";
        if (settings.getCurrentLevel() == 0 && !PlayerPrefs.HasKey("level2"))
        {
            PlayerPrefs.SetInt("level2", 1);
            return;
        }
        if (settings.getCurrentLevel() == 1 && !PlayerPrefs.HasKey("level3"))
        {
            PlayerPrefs.SetInt("level3", 1);
            return;
        }
        if (settings.getCurrentLevel() == 2 && !PlayerPrefs.HasKey("level4"))
        {
            PlayerPrefs.SetInt("level4", 1);
            return;
        }
    }
}
