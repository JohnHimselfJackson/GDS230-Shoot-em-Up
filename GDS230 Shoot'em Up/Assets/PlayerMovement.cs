using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public Joystick joystick;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    void Update()
    {
        horizontalMove = joystick.Horizontal * runSpeed;

        //Joystick up
        float verticalMove = joystick.Vertical;

        //If joystick is pointing up then jump is true
        if (verticalMove >= .5f)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        //Character move & jump
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
