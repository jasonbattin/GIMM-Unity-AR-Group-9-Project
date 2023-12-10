
using UnityEngine;
using UnityEngine.EventSystems;

//makes a button that can be used for mobile controls
public class MovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ButtonControls buttonControls;
    [SerializeField] float dirX;
    [SerializeField] bool isJumpButton;
    [SerializeField] bool isDashButton;
    [SerializeField] bool isMoveButton;

    //when button is pressed, process it as input based on the type of button
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isJumpButton)
            buttonControls.jumpKey = true;
        if (isDashButton)
            buttonControls.dashKey = true;
        if (isMoveButton)
            buttonControls.HorizontalButton(dirX);
        buttonControls.notUsingInput = true;
        Debug.Log("Button pressed");
    }

    //when button is released, reset input
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isJumpButton)
            buttonControls.jumpKey = false;
        if (isDashButton)
            buttonControls.dashKey = false;
        if (isMoveButton)  
            buttonControls.HorizontalButton(0);
        buttonControls.notUsingInput = false;
    }

    // void Update()
    // {
    //     foreach (Touch touch in Input.touches)
    //     {
    //         /*if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
    //         {*/
               

    //             // Check if this point is within the bounds of the button
    //             if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), touch.position, Camera.main))
    //             {
    //                 // The touch began on this button
    //                 // Process it as input based on the type of button
    //                 if (isJumpButton)
    //                     buttonControls.jumpKey = true;
    //                 if (isDashButton)
    //                     buttonControls.dashKey = true;
    //                 if (isMoveButton)
    //                     buttonControls.HorizontalButton(dirX);
    //                 buttonControls.notUsingInput = true;
    //                 Debug.Log("Button pressed");
    //                 return;
    //             }
    //         }

            
           
               
    //         // Reset the input
    //         if (isJumpButton)
    //             buttonControls.jumpKey = false;
    //         if (isDashButton)
    //             buttonControls.dashKey = false;
    //         if (isMoveButton)
    //             buttonControls.HorizontalButton(0);
    //         buttonControls.notUsingInput = false;
    // }
}
