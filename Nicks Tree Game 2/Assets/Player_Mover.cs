using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mover : MonoBehaviour {

    [SerializeField]
    private float speedH;

    [SerializeField]
    private float flightTime;

    [SerializeField]
    private float flightPower;

    private float flightTimer = 0;

    Vector3 velocity = Vector3.zero;




    CharacterController pawn;

	void Start () {
        pawn = GetComponent<CharacterController>();
        flightTimer = flightTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Get the mouse position relative to about the player position, will be used for flight
        //print(Input.mousePosition);
        int screenW = Screen.width;
        int screenH = Screen.height;
        int mouseX = (int)Input.mousePosition.x - (screenW / 2);
        int mouseY = (int)Input.mousePosition.y - (screenH / 3);


        //get user input for horizontal axis
        float axisH = Input.GetAxis("Horizontal");
        //set initial velocity heading
        Vector3 heading = new Vector3(0, 0, -axisH * speedH);

        velocity.x = heading.x;
        velocity.z = heading.z;

        if( pawn.isGrounded ) {
            //reset the timer
            flightTimer = flightTime;
        } else {
            //tick the timer
            flightTimer -= Time.deltaTime;
        }

        //if they're clicking the mouse
        if( Input.GetMouseButton(0) ) {
            if (flightTimer > 0 ) {
                //we get a directional vector towards the mouse.
                //because the camera is sideways, it's actually the Z axis that is "left and right", and "forwards" is to the negative Z
                Vector3 flightHeading = Vector3.Normalize(new Vector3(0, mouseY, -mouseX));
                velocity += flightHeading * flightPower * Time.deltaTime;
            }
        }

        velocity += Physics.gravity * Time.deltaTime;

        pawn.Move(velocity * Time.deltaTime);

	}
}
