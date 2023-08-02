using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    string scoreText;
    int score;
    float currentTime = 0f;
    float startingTime = 60f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Timer") {
            currentTime = startingTime;
        }
        if (gameObject.tag == "Score")
        {
            score = 0;
            scoreText = "Score : ";
            GetComponent<TMP_Text>().text += scoreText + score;
        }
    }

    private void Update()
    {
        if (gameObject.tag == "Timer")
        {
            currentTime -= 1 * Time.deltaTime;
            gameObject.GetComponent<TMP_Text>().text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsManager>().unlockDifficulties();
                finishDisplay("WinUI");
                currentTime = 0;
            }
        }
    }

    public void finishDisplay(string canvasToLoad)
    {
        GameObject[] allPathCube = GameObject.FindGameObjectsWithTag("PathCube");
        MapGenerator mapManagerGO = FindObjectOfType<MapGenerator>();
        Destroy(mapManagerGO.gameObject);
        foreach (GameObject pathCube in allPathCube)
        {
            Destroy(pathCube.gameObject);
        }
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        Destroy(playerGO.gameObject);
        Canvas scoreCanvas = FindObjectOfType<Canvas>();
        Instantiate(Resources.Load<Canvas>(canvasToLoad), new Vector3(0, 0, 0), Quaternion.identity);
        Destroy(scoreCanvas.gameObject);
    }

    public void addScore()
    {
        score += 1;
        string newText = scoreText + score;
        GetComponent<TMP_Text>().text = newText;
    }

    void OnDestroy()
    {
        if (gameObject.tag == "Score")
        {
            UIManager.addYourScore(score);
        }
    }
}
