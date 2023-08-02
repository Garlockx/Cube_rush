using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private Dictionary<string, bool> isKeysPress;
    private void Start()
    {
        SettingsManager settingManager = FindObjectOfType<SettingsManager>().GetComponent<SettingsManager>();
        isKeysPress = new Dictionary<string, bool>
        {
            {settingManager.keyButton["BlueKey"], false },
            {settingManager.keyButton["PurpleKey"], false },
            {settingManager.keyButton["OrangeKey"], false },
            {"right", false },
            {"down", false },
        };
    }

    public void displayTutorial(string input)
    {
        List<Path> pathCubeList = FindObjectOfType<MapGenerator>().GetComponent<MapGenerator>().pathCubeList;
        List<string> keyToPress = FindObjectOfType<Gameplay>().GetComponent<Gameplay>().getKeyToPress();
        if (isKeysPress.ContainsKey(input) && isKeysPress[input] == false)
        {
            int index = keyToPress.FindIndex(x => x == input);
            if (input != "right" && input != "down")
            {
                Path firstCube = pathCubeList[index + 1];
                createText(input, firstCube, new Vector3(-0.4f, 2.0f, 0.0f), input);
            } 
            if (input == "right")
            {
                Path firstCube = pathCubeList[index];
                createText(input, firstCube, new Vector3(0.6f, 2.0f, 0.0f), "→");
            }
            if (input == "down")
            {
                Path firstCube = pathCubeList[index - 1];
                createText(input, firstCube, new Vector3(1.6f, 1.0f, 0.0f), "↓");
            } 
            isKeysPress[input] = true;
        }
    }


    public void createText(string input, Path cube, Vector3 position, string display)
    {
        GameObject tutorialGO = new GameObject(input);
        tutorialGO.transform.SetParent(cube.gameObject.transform);
        tutorialGO.transform.localPosition = Vector3.zero;

        TextMesh tutorialText = tutorialGO.AddComponent<TextMesh>();
        tutorialText.text = display.ToUpper();
        tutorialText.characterSize = 0.1f;
        tutorialText.fontSize = 100;
        tutorialText.gameObject.transform.localPosition = position;
    }
}
