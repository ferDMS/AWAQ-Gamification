using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    private Vector3 originalPosition;

    [SerializeField]
    private GameObject player;

    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;

    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
        position.z = player.transform.position.z;
        player.transform.position= position;

        // Start the Coroutine to reset the position after 1 second
        StartCoroutine(ResetPositionAfterDelay());
    }
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ResetPositionAfterDelay()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(0.1f);

        // Reset the player's position to the original position
        player.transform.position = originalPosition;
    }
}
