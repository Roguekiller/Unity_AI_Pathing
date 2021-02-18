using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour {

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    //Rotate about the X axis
    float xRotation = 0f;

	// Use this for initialization
	void Start () {
        //Cursor will be locked and won't leave the window of the game.
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        //Creating float for both X and Y axis.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotates the character about the X axis. += = Inversion
        xRotation -= mouseY;

        //Clamps player rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Tracking the roation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Acts as left and right movement for the mouse.
        playerBody.Rotate(Vector3.up * mouseX);
	}
}
