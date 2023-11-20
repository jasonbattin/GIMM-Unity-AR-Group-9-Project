
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private int baseSpeed;
    [SerializeField] private int dashPower;
    [SerializeField] private float dashTime; //how long to dash
    private float dashTimer; //time player has been dashing
    public enum CurrentState { idle, walkingLeft, walkingRight, dash, jump, fall};
    private CurrentState currentState = CurrentState.idle;
    
    private float dirX;

    private int jumps;

    //here in case it needs to be accessed by animations or something
    public CurrentState CurrentState1 { get => currentState;}

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        
    }

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
