using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, string> keyButton;
    public float pressTimer;
    private float[] timerByDifficulties;
    private Dropdown diffDropDown;
    private GameObject settingsGameObject;
    private Button buttonToRebind = null;


    // Start is called before the first frame update
    void Start()
    {
        settingsGameObject = GameObject.FindGameObjectWithTag("Settings");
        diffDropDown = FindObjectOfType<Dropdown>();
        keyButton = new Dictionary<string, string>();
        
        setInput();
        if (GameObject.FindGameObjectWithTag("SettingsUI"))
        {
            setButtonText();
        }
        setTimerDifficulties();
        if (PlayerPrefs.HasKey("Timer"))
        {
            pressTimer = PlayerPrefs.GetFloat("Timer");
        }
        else
        {
            pressTimer = timerByDifficulties[0];
        }
        if (diffDropDown != null)
        {
            setDropdownOptions();
        }
    }

    private void Update()
    {
        GameObject explanation = GameObject.FindGameObjectWithTag("Explanation");
        if (explanation != null)
        {
            explanation.GetComponent<TMP_Text>().text = "You have " + PlayerPrefs.GetFloat("Timer") + " seconds between each input";
        }

        if (buttonToRebind != null)
        {
            if (Input.anyKeyDown)
            {
                settingsGameObject.GetComponent<SettingsManager>().keyButton[buttonToRebind.name] = Input.inputString;
                buttonToRebind.GetComponentInChildren<Text>().text = Input.inputString.ToUpper();
                PlayerPrefs.SetString(buttonToRebind.name, Input.inputString);
                buttonToRebind = null;
            }
        }
    }

    private void setTimerDifficulties()
    {
        timerByDifficulties = new float[4];
        timerByDifficulties[0] = 5.0f;
        if (PlayerPrefs.HasKey("level2"))
        {
            timerByDifficulties[1] = 2.0f;
        }
        if (PlayerPrefs.HasKey("level3"))
        {
            timerByDifficulties[2] = 1.0f;
        }
        if (PlayerPrefs.HasKey("level4"))
        {
            timerByDifficulties[3] = 0.5f;
        }
    }

    private void setDropdownOptions()
    {
        diffDropDown.ClearOptions();
        List<Dropdown.OptionData> dropDownOptions = new List<Dropdown.OptionData>();
        int diff = 0;
        for (int i = 0; i < timerByDifficulties.Length; i++)
        {
            if (timerByDifficulties[i] != 0)
            {
                Dropdown.OptionData newData = new Dropdown.OptionData();
                newData.text = "Level " + (i + 1);
                dropDownOptions.Add(newData);
            }
            if (timerByDifficulties[i] == pressTimer)
            {
                diff = i;
            }
        }
        diffDropDown.AddOptions(dropDownOptions);
        diffDropDown.value = diff;
    }

    private void setInput()
    {
        if (PlayerPrefs.HasKey("BlueKey"))
        {
            keyButton.Add("BlueKey", PlayerPrefs.GetString("BlueKey"));
        }
        else
        {
            keyButton.Add("BlueKey", "b");
        }
        if (PlayerPrefs.HasKey("OrangeKey"))
        {
            keyButton.Add("OrangeKey", PlayerPrefs.GetString("OrangeKey"));
        }
        else
        {
            keyButton.Add("OrangeKey", "o");
        }
        if (PlayerPrefs.HasKey("PurpleKey"))
        {
            keyButton.Add("PurpleKey", PlayerPrefs.GetString("PurpleKey"));
        }
        else
        {
            keyButton.Add("PurpleKey", "p");
        }
    }

    private void setButtonText()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            if (keyButton.ContainsKey(button.name))
            {
                button.GetComponentInChildren<Text>().text = settingsGameObject.GetComponent<SettingsManager>().keyButton[button.name].ToUpper();
            }
        }
    }

    public void changeDifficulties(Dropdown dropDownGO)
    {
        PlayerPrefs.SetFloat("Timer", timerByDifficulties[dropDownGO.value]);
        GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsManager>().pressTimer = timerByDifficulties[dropDownGO.value];
    }

    public void unlockDifficulties()
    {

    }

    public void changeInput(Button button)
    {
        buttonToRebind = button;
    }

    public int getCurrentLevel()
    {
        return System.Array.IndexOf(timerByDifficulties, pressTimer);
    }

}
