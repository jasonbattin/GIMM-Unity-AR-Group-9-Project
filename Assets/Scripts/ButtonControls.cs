
using UnityEngine;

public class ButtonControls : MonoBehaviour
{
    public bool dashKey = false;
    public bool jumpKey = false;
    public float dirX = 0;
    public bool notUsingInput = false;

    //horizontal movement
    public void HorizontalButton(float dirX)
    {
        this.dirX = dirX;
    }

    //if using normal input, process input
    private void Update()
    {

        if (!notUsingInput)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            HorizontalButton(horizontal);

            if (Input.GetButtonDown("Jump"))
            {
                jumpKey = true;
            }
            else
            {
                jumpKey = false;
            }
            if (Input.GetButtonDown("Dash"))
            {
                dashKey = true;
            }
            else
            {
                dashKey = false;
            }
        }
    }
}
