using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float upDownRange = 90;
    public float jumpSpeed = 5f;
    public float downSpeed = 5;

    private Vector3 speed;
    private float forwardSpeed;
    private float sideSpeed;

    private float rotLeftRight;
    private float rotUpDown;
    private float verticalRotation = 0f;

    private float verticalVelocity = 0f;

    private CharacterController cc;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        FPMove();
        FPRotate();

	}

    void FPMove()
    {
        forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        /* locked jump issue
        if (cc.isGrounded && Input.GetButtonDown("Jump"))
            verticalVelocity = jumpSpeed;
        if (!cc.isGrounded)
            verticalVelocity = downSpeed;
        */

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;

        cc.Move(speed * Time.deltaTime);
        }

    void FPRotate()
    {
        //좌우회전
        rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, rotLeftRight, 0f);

        //상하회전
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

    }
}
