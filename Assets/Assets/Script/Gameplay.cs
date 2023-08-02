using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private float currentPressTimer;
    private bool isTimerStart = false;
    private PlayerMove playerMove;
    private int pathCount;
    private List<string> keyToPress = new List<string>();
    private string myDirection = "right";

    // Start is called before the first frame update
    void Start()
    {
        playerMove = gameObject.GetComponent<PlayerMove>();
        pathCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMove.isMoving)
        {
            return;
        }

        currentPressTimer -= 1 * Time.deltaTime;
        if (currentPressTimer <= 0 && isTimerStart == true)
        {
            FindObjectOfType<InGameUIManager>().finishDisplay("GameOverUI");
        }
        if (Input.anyKeyDown)
        {
            isRightKeyPress();
        }
    }

    public void addKeyToPress(string input)
    {
        keyToPress.Add(input);
        TutorialManager tutorial = FindObjectOfType<TutorialManager>();
        if (tutorial != null)
        {
            tutorial.GetComponent<TutorialManager>().displayTutorial(input);
        }
    }

    public List<string> getKeyToPress()
    {
        return keyToPress;
    }

    public void isRightKeyPress()
    {
        string newDirection = null;
        if (isTimerStart == false)
        {
            isTimerStart = true;
        }
        if (Input.GetKeyDown(keyToPress[pathCount]))
        {
            if (keyToPress[pathCount] == "right" || keyToPress[pathCount] == "left" || keyToPress[pathCount] == "down" || keyToPress[pathCount] == "up")
            {
                if (myDirection != keyToPress[pathCount])
                {
                    newDirection = keyToPress[pathCount];
                }
                myDirection = keyToPress[pathCount];                
            }
            GameObject.FindGameObjectWithTag("Score").GetComponent<InGameUIManager>().addScore();
            if (myDirection == "right")
            {
                playerMove.Assemble(Vector3.up, new Vector3(1, -1, 0), newDirection);
            } else if (myDirection == "down")
            {
                playerMove.Assemble(Vector3.down, new Vector3(-1, -1, 0), newDirection);
            } else if (myDirection == "left")
            {
                playerMove.Assemble(Vector3.down, new Vector3(-1, 1, 0), newDirection);
            } else
            {
                playerMove.Assemble(Vector3.up, new Vector3(1, 1, 0), newDirection);
            }
            currentPressTimer = FindObjectOfType<SettingsManager>().pressTimer;
            pathCount += 1;
        } else
        {
            FindObjectOfType<InGameUIManager>().finishDisplay("GameOverUI");
        }
    }
}
