
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    public bool dashKey = false;
    public bool jumpKey = false;
    public float dirX = 0;
    public Playercontrols controls; //controls for the player, used to get input
    private float resetTimer;
    private float resetDelay = 0.1f;


    private void Start()
    {
        controls = new Playercontrols();
        controls.Enable();
        //reading the values of the player movement direction for the players movement.
        controls.Ground.Move.performed += moving =>
        {
            dirX = moving.ReadValue<float>();
        };
        //reading the value of the jump key for the players jump.
        controls.Ground.Jump.performed += jump =>
        {
            jumpKey = true;
        };
        controls.Ground.Jump.canceled += jump =>
        {
            jumpKey = false;
        };
        
        //reading the value of the dash key for the players dash.
        controls.Ground.Dash.performed += dash =>
        {
            dashKey = true;
        };
        controls.Ground.Dash.canceled += dash =>
        {
            dashKey = false;
        };
        
    }

    private void Update()
    {
        
        if (dashKey || jumpKey)
        {
            resetTimer += Time.deltaTime;
            if (resetTimer >= resetDelay)
            {
                resetTimer = 0;
                dashKey = false;
                jumpKey = false;
            }
        }else
        {
            resetTimer = 0;
        }
    }
}
