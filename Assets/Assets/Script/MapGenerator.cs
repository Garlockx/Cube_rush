using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int pathPreload = 5;
    public List<Path> pathCubeList;
    private Vector3 directionPath;
    private int cubeSinceLastChange;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0.0f, -9.81f, 0.0f);
        cubeSinceLastChange = 0;
        directionPath = Vector3.right;
        pathCubeList = new List<Path>();
        loadPlayer();
        loadPath();     
    }

    private void loadPlayer()
    {
        Instantiate(Resources.Load<PlayerMove>("Player"), new Vector3(0, 0.9f, 0), Quaternion.identity);
    }

    private void loadPath()
    {
        for (int i = 0; i < pathPreload; i++)
        {
            Path newPathCube = Instantiate(Resources.Load<Path>("Path"), new Vector3(i, 0, 0), Quaternion.identity);
            if (i != 0)
            {
                newPathCube.first = false;
            }
            else
            {
                newPathCube.first = true;
            }
            pathCubeList.Add(newPathCube);
        }
    }

    public void findPath()
    {
        bool isChangingDirection;
        string firstKey = null;
        string secondKey = null;
        if (cubeSinceLastChange == 10)
        {
            isChangingDirection = (Random.value > 0.7f);
            if (isChangingDirection == true)
            {
                if (directionPath == Vector3.right)
                {
                    directionPath = Vector3.down;
                    firstKey = "right";
                    secondKey = "down";
                }
                else if (directionPath == Vector3.left)
                {
                    directionPath = Vector3.up;
                    firstKey = "left";
                    secondKey = "up";
                }
                else if (directionPath == Vector3.down)
                {
                    directionPath = Vector3.left;
                    firstKey = "down";
                    secondKey = "left";
                }
                else
                {
                    directionPath = Vector3.right;
                    firstKey = "up";
                    secondKey = "right";
                }
            }
            cubeSinceLastChange = 0;
        }
        createPathCube(directionPath, firstKey, secondKey);
    }

    public void createPathCube(Vector3 direction, string firstKey, string secondKey)
    {
        GameObject lastPathCube = pathCubeList[pathCubeList.Count - 1].gameObject;
        Vector3 lastPathCubePosition = lastPathCube.transform.position;

        Vector3 newPathCubePosition = lastPathCubePosition;
        newPathCubePosition += direction;

        Path newPathCube = Instantiate(Resources.Load<Path>("Path"), newPathCubePosition, Quaternion.identity);
        if (firstKey != null && secondKey != null)
        {
            FindObjectOfType<Gameplay>().GetComponent<Gameplay>().addKeyToPress(firstKey);
            FindObjectOfType<Gameplay>().GetComponent<Gameplay>().addKeyToPress(secondKey);
            lastPathCube.GetComponent<Path>().isDirectionToTrue();
        }
        pathCubeList.Add(newPathCube);
        cubeSinceLastChange += 1;
    }
}
