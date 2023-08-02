using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float rollSpeed = 3;
    public bool isMoving;
    public AudioSource cubeSound;


    public void changePlayerColor(Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color; 
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }


    public void Assemble(Vector3 Vertical, Vector3 direction, string newDirection)
    {
        Vector3 actualPosition = new Vector3();

        actualPosition.x = Mathf.Round(transform.position.x * 10.0f) * 0.1f;
        actualPosition.y = Mathf.Round(transform.position.y * 10.0f) * 0.1f;
        actualPosition.z = Mathf.Round(transform.position.z * 10.0f) * 0.1f;

        Vector3 rotationCenter = transform.position +  direction * 0.5f;
        Vector3 rotationAxis = Vector3.Cross(Vertical, direction);
        StartCoroutine(Roll(rotationCenter, rotationAxis, newDirection));
    }

    IEnumerator Roll(Vector3 rotationCenter, Vector3 rotationAxis, string newDirection)
    {
        isMoving = true;
        for (int i = 0; i < (90 / rollSpeed); i++)
        {
            transform.RotateAround(rotationCenter, rotationAxis, rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        if (newDirection == "down")
        {
            Physics.gravity = new Vector3(-9.81f, 0.0f, 0.0f);
            Vector3 actualPosition = transform.position;
            actualPosition.x -= 0.1f;
            actualPosition.y += 0.1f;
            transform.position = actualPosition;
        } else if (newDirection == "left")
        {
            Physics.gravity = new Vector3(0.0f, 9.81f, 0.0f);
            Vector3 actualPosition = transform.position;
            actualPosition.x += 0.1f;
            actualPosition.y += 0.1f;
            transform.position = actualPosition;
        } else if (newDirection == "up")
        {
            Physics.gravity = new Vector3(9.81f, 0.0f, 0.0f);
            Vector3 actualPosition = transform.position;
            actualPosition.x += 0.1f;
            actualPosition.y -= 0.1f;
            transform.position = actualPosition;
        } else if (newDirection == "right")
        {
            Physics.gravity = new Vector3(0.0f, -9.81f, 0.0f);
            Vector3 actualPosition = transform.position;
            actualPosition.x -= 0.1f;
            actualPosition.y -= 0.1f;
            transform.position = actualPosition;
        }
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PathCube" && other.GetComponent<Path>().first == false)
        {
            cubeSound.Play();
        }
    }
}
