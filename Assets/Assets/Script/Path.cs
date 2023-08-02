using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct PathCube
{
    public Color32 color;
}

public class Path : MonoBehaviour
{
    public PathCube pathCube;
    private Color32[] colors;
    
    /*
     3 Colors :
        blue : #3330E4 // rgb(51, 48, 228, 255)
        purple : #F637EC // rgb(246, 55, 236, 255)
        orange : #FBB454 // rgb(251, 180, 84, 255)
     */
    Color32 blue = new Color32(51, 48, 228, 255);
    Color32 purple = new Color32(246, 55, 236, 255);
    Color32 orange = new Color32(251, 180, 84, 255);
    private string[] keyCodes;
    public bool first;
    private MapGenerator mapGenerator;
    private SettingsManager settingManager;
    public bool isChangingDirection;


    // Start is called before the first frame update
    void Start()
    {
        //isChangingDirection = false;
        mapGenerator = FindObjectOfType<MapGenerator>().GetComponent<MapGenerator>();
        settingManager = FindObjectOfType<SettingsManager>().GetComponent<SettingsManager>();
        pathCube = new PathCube();
        colors = new Color32[3];
        colors[0] = blue;
        colors[1] = purple;
        colors[2] = orange;

        keyCodes = new string[3];
        keyCodes[0] = settingManager.keyButton["BlueKey"];
        keyCodes[1] = settingManager.keyButton["PurpleKey"];
        keyCodes[2] = settingManager.keyButton["OrangeKey"];
        addColor();
    }

    public void addColor()
    {
        if (first == false)
        {
            int indexRandom = Random.Range(0, colors.Length);
            gameObject.GetComponent<MeshRenderer>().material.color = colors[indexRandom];
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", colors[indexRandom]);
            pathCube.color = colors[indexRandom];
            FindObjectOfType<Gameplay>().GetComponent<Gameplay>().addKeyToPress(keyCodes[indexRandom]);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
        }
    }

    public void isDirectionToTrue()
    {
        gameObject.GetComponent<Path>().isChangingDirection = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && isChangingDirection == true)
        {
            isChangingDirection = false;
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            mapGenerator.findPath();
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(destroyObject());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().changePlayerColor(GetComponent<MeshRenderer>().material.color);
        }
    }

    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject.gameObject);
    }
}
