using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float cameraZ = -10.0f;
    private GameObject playerGameObject;
    private Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            followPlayer();
        }
    }

    private void followPlayer ()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");

        cameraPosition = transform.position;
        cameraPosition.x = playerGameObject.transform.position.x;
        cameraPosition.y = playerGameObject.transform.position.y;
        cameraPosition.z = cameraZ;

        transform.position = cameraPosition;
    }
}
