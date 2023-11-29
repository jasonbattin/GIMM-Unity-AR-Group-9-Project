using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerMovementTransform; //a Transform reference to the player object that the camera should follow

    [Header("Flip Rotation Stats")]
    [SerializeField] private float flipYRotationTime = 0.5f; //a serialized float to define the time it takes for the camera to flip its Y rotation

    private PlayerMovement playerMovement; //a reference to the player movement script
    private bool isFacingRight;


    //OnEnable() and OnDisable():These methods subscribe and unsubscribe the OnSceneLoaded method to the sceneLoaded event of SceneManager,
    //ensuring that the camera updates its reference to the player every time a new scene is loaded.
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdatePlayerReference();
    }

    private void Awake()
    {
        UpdatePlayerReference();
    }

    private void UpdatePlayerReference()
    {
        playerMovementTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerMovementTransform != null)
        {
            playerMovement = playerMovementTransform.GetComponent<PlayerMovement>();
            isFacingRight = playerMovement.IsFacingRight;
        }
    }

    private void Update()
    {
        if (playerMovementTransform != null)
        {
            // Make the CameraFollowObject follow the player's position
            transform.position = playerMovementTransform.position;
        }
    }

    public void CallTurn()
    {
        if (playerMovementTransform == null)
        {
            UpdatePlayerReference();
        }

        LeanTween.rotateY(gameObject, DetermineEndRotation(), flipYRotationTime).setEaseInOutSine();//used to smoothy rotate the camera to the correct rotation
    }

    private float DetermineEndRotation()// check if player is facing right or left and return the correct rotation
    {
        isFacingRight = !isFacingRight;

        return isFacingRight ? 180f : 0f;
    }
}
