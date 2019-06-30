using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    

    public int rotSpeed = 30;
    public int fast = 100, normal = 30, slow = 5;

    public Transform whiteBall;




    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(whiteBall.position, new Vector3(0, -1, 0), rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(whiteBall.position, new Vector3(0, 1, 0), rotSpeed * Time.deltaTime);
        }
        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.RotateAround(whiteBall.position, new Vector3(0, 0, 1), rotSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.RotateAround(whiteBall.position, new Vector3(0, 0, -1), rotSpeed * Time.deltaTime);
        }*/

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rotSpeed = fast;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            rotSpeed = slow;
        }
        else
        {
            rotSpeed = normal;
        }
    }
}
