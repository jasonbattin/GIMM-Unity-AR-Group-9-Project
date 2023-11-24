using System.ComponentModel;
using UnityEngine;
using Vuforia;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private int baseSpeed;
    [SerializeField] private int dashPower;
    [SerializeField] private float dashTime; //how long to dash
    private float dashTimer; //time player has been dashing
    public enum CurrentState { idle, walkingLeft, walkingRight, dash, jump, fall};
    private CurrentState currentState = CurrentState.idle;
    
    private float dirX; // Horrizontal movement input direction -1 = left, 1 = right, 0 = none.    just checking if i understand this correctly?-jason
    private int jumps;
    public bool IsFacingRight { get; private set; } //here in case it needs to be accessed by animations or something (currently using it for cinemachine stuff-jason)


    [Header("Camera")]
    private CameraFollowObject _cameraFollowObject; //a reference to the CameraFollowObject component on the camera follow object game object
    [SerializeField] private GameObject _cameraFollowObjectGameObject;  //a reference to the camera follow object game object
    private float _fallSpeedYDampingChangeThreshold; //the threshold at which the camera will lerp the Y damping when the player is falling





    public CurrentState getCurrentState()
    {
        return currentState;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set the flag indicating the start direction of the player 
        IsFacingRight = true;

        #region INITIALIZE CAMERA
        // Get the CameraFollowObject component from the camera follow object game object
        _cameraFollowObject = _cameraFollowObjectGameObject.GetComponent<CameraFollowObject>();
        _fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;
        #endregion
    }

    // Update is called once per frame
    private void Update()
    {

        //retrieve horizontal axis
        dirX = Input.GetAxisRaw("Horizontal");

        //switch to run corrisponding code depending on state
        switch (currentState)
        {
            case CurrentState.walkingLeft:
            case CurrentState.walkingRight:
            case CurrentState.idle:
                idleAndWalkingState();
                break;
            case CurrentState.dash:
                dashState();
                break;
            case CurrentState.jump:
                jumpState();
                break; 
            case CurrentState.fall:
                fallState();
                break;
        }

        #region CAMERA LERP Y DAMPING
        if (rb.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }
        if (rb.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            //reset so it can be called again
            CameraManager.instance.LerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
        #endregion
    }

    #region NEW CODE USED FOR CAMERA AND SPRITE FLIPPING
    // Called every physics update, used for movement
    private void FixedUpdate()
    {
        if (dirX != 0) //if were detecting movement, turn the player to face the direction they are moving
        {
            CheckDirectionToFace(dirX > 0);//only call the method if the player is moving left
        }
    }
    public void CheckDirectionToFace(bool isMovingRight)//determins if the player is moving left or right
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }
    private void Turn()//turns the player to face the direction they are moving, we use rotation because it changes the transform, which is what the camera offset is based on
    {
        // Get the current Euler angles of the player
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Create a new rotation vector by adding 180 degrees to the Y-axis component
        Vector3 newRotation = new Vector3(currentRotation.x, currentRotation.y + 180f, currentRotation.z);

        // Apply the new rotation to the player
        transform.rotation = Quaternion.Euler(newRotation);

        // Call the turn method on the CameraFollowObject
        if (_cameraFollowObject != null)
        {
            _cameraFollowObject.CallTurn();
        }

        // Toggle the flag indicating the direction the character is facing
        IsFacingRight = !IsFacingRight;
    }
    #endregion

    //checks if can jump, if so, jumps 
    private int jump()
    {
        if (Input.GetButtonDown("Jump") && jumps < 2)
        {
            currentState = CurrentState.jump;
            rb.velocity = new Vector2(dirX * baseSpeed, baseSpeed * 2);
            return 1;
        }
        return 0;
    }

    //fall state
    private void fallState()
    {
        
        //fall fast
        rb.gravityScale = 2;
        //if no longer falling, return to idle
        if (rb.velocity.y >= -0.01f)
        {
            currentState = CurrentState.idle;
        }

        //if hasnt double jumped yet, check for jump
        jumps += jump();

    }

    //jump state
    private void jumpState()
    {

        //if falling, fall
        if (rb.velocity.y < -0.01f)
        {
            currentState = CurrentState.fall;
        }

        //if hasnt double jumped yet, check for jump

        jumps += jump();

    }


    //dash state
    private void dashState()
    {
        //if the player has been dashing for long enough, exit dash, otherwise continue dashing
        dashTimer += Time.deltaTime;
        if (dashTimer < dashTime)
        {
            //dash will deactivate gravity
            rb.gravityScale = 0;
            rb.velocity = new Vector2(dirX * baseSpeed * dashPower, rb.velocity.y);
        }
        else
        {
            //reset to idle if time is up
            currentState = CurrentState.idle;
            dashTimer = 0;
        }
    }

    //idle state
    //buttons are temporary until we get AR added in, I also dont have android rn, but ill remedy that soon!
    private void idleAndWalkingState()
    {
        //return to normal state
        jumps = 0;
        rb.gravityScale = 1;

        //if falling, enter fall state
        if (rb.velocity.y < -0.01f)
        {
            currentState = CurrentState.fall;
            return;
        }

        //jump if corrisponding key is pressed
        jumps += jump();

        //if has vertical velocity, but still idle/walking, enter jump
        if (rb.velocity.y > 0.01f)
        {
            currentState = CurrentState.jump;
            return;
        }

        //if shift is pressed, dash
        if (Input.GetKeyDown("left shift"))
        {
            currentState = CurrentState.dash;
            return;
        }

        horizontalMovement();
    }

    //horizontal movement and checks
    private void horizontalMovement()
    {
        //base walking/idle on dirX
        if (dirX > 0)
        {
            currentState = CurrentState.walkingRight;
        }
        else if (dirX < 0)
        {
            currentState = CurrentState.walkingLeft;
        }
        else
        {
            currentState = CurrentState.idle;
        }
        //calculate horizontal velocity
        rb.velocity = new Vector2(dirX * baseSpeed, rb.velocity.y);
    }
}
