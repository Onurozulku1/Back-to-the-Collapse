using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 moveVector;

    void OnMovement(InputValue movementInput)
    {
        moveVector = movementInput.Get<Vector2>();
    }

    void OnInteract(InputValue interactInput)
    {
        if (GameManager.instance.InteractedObject != null)
            GameManager.instance.InteractedObject.Interact();

    }

    void OnCrouch(InputValue crouchInput)
    {
        GetComponent<PlayerMovement>().isCrouching = !GetComponent<PlayerMovement>().isCrouching;
        if (GetComponent<PlayerMovement>().isCrouching)
            Debug.Log("crouching");

    }


}
