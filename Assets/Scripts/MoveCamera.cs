using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    public float speed;

    public GameObject upperBorders;
    public GameObject lowerBorders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x + 9 < upperBorders.transform.position.x)
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x - 9 > lowerBorders.transform.position.x)
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y - 5 > lowerBorders.transform.position.y)
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.position.y + 5 < upperBorders.transform.position.y)
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x + 9 < upperBorders.transform.position.x)
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x - 9 > lowerBorders.transform.position.x)
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y - 5 > lowerBorders.transform.position.y)
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y + 5 < upperBorders.transform.position.y)
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
        }
    }
}
