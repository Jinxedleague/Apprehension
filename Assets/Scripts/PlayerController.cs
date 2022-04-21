using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour                      // created/compiled by Matthew Gumprecht
{
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float sprintMultiplier = 0.8f;
    [SerializeField] private float jumpHeight = 0.7f;
    [SerializeField] private float playerGravity = -9.81f;        //force of gravity on earth
    [SerializeField] private float groundCheckRadius = 0.6f;
    [SerializeField] private float crouchHeight = 1.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchTime = 0.15f;
    [SerializeField] private Vector3 crouchedControllerCenter = new Vector3(0f, .5f, 0f);
    [SerializeField] private Vector3 standingControllerCenter = new Vector3(0f, 0f, 0f);
    [SerializeField] private GameObject player;

    public Transform playerModel, playerCamera, playerGroundCheck;
    public CharacterController characterController;
    public LayerMask groundMask;

    float cameraRotationAroundX = 0f;
    Vector3 currentVelocity;
    bool isOnGround;
    bool isCrouched;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;                                                         //Disable the mouse and lock it to the screen.
    }

    // Update is called once per frame
    void Update()
    {
        float lookX = Input.GetAxis("Mouse X") * mouseSensitivity;                                    //Gathers player horizontal mouse input
        float lookY = Input.GetAxis("Mouse Y") * mouseSensitivity;                                    //Gathers player vertical mouse input

        float moveX = Input.GetAxis("Horizontal");                                                    //Gathers player horizontal movement input (A/D or LeftArrow/RightArrow)
        float moveZ = Input.GetAxis("Vertical");                                                      //Gathers player vertical movement input   (W/S or UpArrow/DownArrow)


        //Is Player Grounded?
        isOnGround = Physics.CheckSphere(playerGroundCheck.position, groundCheckRadius, groundMask);  //At the groundCheck's position, project a sphere or groundCheckRadius size that looks to collide with anything in the groundMask (isGrounded = true)
        if (isOnGround && currentVelocity.y < 0f)                                                     //To prevent our gravity velocity from accumulating while not falling, reset currentVelocity.y when on the ground
        {
            currentVelocity.y = -2f;                                                                  //-2f used as reset point in the event physics sphere collides before model touches the ground, ensuring player is grounded
        }


        //Player Horizontal Look
        playerModel.Rotate(Vector3.up * lookX);                                                       //Rotates player around Vector3.up by the gathered horizontal input


        //Player Vertical Look
        cameraRotationAroundX -= lookY;                                                               //Subtract the value of the camera's angle around X axis by vertical input
        cameraRotationAroundX = Mathf.Clamp(cameraRotationAroundX, -90f, 90f);                        //Clamp camera rotation to 90 up and down
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationAroundX, 0f, 0f);       //Apply rotation


        //Player Movement
        Vector3 movement = transform.right * moveX + transform.forward * moveZ;                       //Creates a new Vector3 and sets its right and forward vectors to user input
        if (movement.magnitude > 1)                                                                   //When moving diagonally, player magnitude is greater due to the combined effect of both horizontal and vertical movement. If this happens...
        {
            movement /= movement.magnitude;                                                           //Set magnitude to 1 so diagonal movement matches purely vertical/horizontal movement (normalizing)
        }
        characterController.Move(movement * movementSpeed * Time.deltaTime);                          //Apply new Vector3 to Character Controller multiplied by speed and deltaTime
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))                               //If LeftShift is pressed while W is down
        {
            characterController.Move(movement * movementSpeed * sprintMultiplier * Time.deltaTime);   //Sprint
        }


        //Player Jump
        if (Input.GetButtonDown("Jump") && isOnGround)                                                //if default "Jump" key is pressed while isOnGround is true, jump force is applied while compensating for deceleration due to gravity
        {
            currentVelocity.y = Mathf.Sqrt(jumpHeight * -3f * playerGravity);                         //v^2 = 2gh for vertical velocity influenced by gravity https://www.aplustopper.com/equations-of-motion/
        }


        //Player Crouch
        if (Input.GetKey(KeyCode.C) && isOnGround)
        {
            StartCoroutine(CrouchToggle());
        }


        //Player Gravity
        currentVelocity.y += playerGravity * Time.deltaTime;                                          //Add gravity float to currentVelocity y value times deltaTime
        characterController.Move(currentVelocity * Time.deltaTime * 2.0f);                            //Apply current velocity (with gravity) to controller  !NOTE: deltaTime multiplied again to represent compounding falling speed with real gravity

        if (player.transform.position.y < 100)
        {
            player.transform.position = new Vector3(0f, 112f, 35f);
        }
    }

    private IEnumerator CrouchToggle()                                                                                          //Coroutine for crouching (NOTE: CameraCenter is referencing the ".center" component of the character controller)
    {
        float timeElapsed = 0.0f;                                                                                               //Set how much time has elapsed
        float desiredCameraHeight = isCrouched ? standingHeight : crouchHeight;                                                 //Set desiredCameraHeight to standingHeight or crouchHeight based on the state of isCrouched
        float currentCameraHeight = characterController.height;                                                                 //Set the currentCameraHeight float to the character controller height
        Vector3 desiredCameraCenter = isCrouched ? standingControllerCenter : crouchedControllerCenter;                         //Set desiredCameraCenter position to standingControllerCenter or crouchControllerCenter based on the state of isCrouched
        Vector3 currentCameraCenter = characterController.center;

        while (timeElapsed < crouchTime)
        {
            characterController.height = Mathf.Lerp(currentCameraHeight, desiredCameraHeight, timeElapsed / crouchTime);        //Set the height of the character controller using Lerp. Lerp between where the height is now and where you want it to be over the course of timeElapsed. timeElapsed being how long crouchTime is.
            characterController.center = Vector3.Lerp(currentCameraCenter, desiredCameraCenter, timeElapsed / crouchTime);      //Set the center of the character controller using Lerp. Lerp between where the center is now and where you want it to be over the course of timeElapsed. timeElapsed being how long crouchTime is.
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = desiredCameraHeight;
        characterController.center = desiredCameraCenter;

        isCrouched = !isCrouched;
    }
}
