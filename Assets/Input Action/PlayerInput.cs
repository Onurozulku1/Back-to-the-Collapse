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

    //UI Input
    void OnBack(InputValue backInput)
    {
        if (!UiManager.instance.DisplayingTabs())
        {
            Debug.Log("Displaying pause menu");
        }
        else
        {
            UiManager.instance.CloseTabs();
        }
    }

    void OnNextDialogue(InputValue nextInput)
    {
        DialogueManager.instance.DisplayNextSentence();
    }


    public Transform CameraParent;
    void OnRotateCamera(InputValue mouseDeltaInput)
    {
        float rotateValue = mouseDeltaInput.Get<Vector2>().x;
        CameraParent.Rotate(Vector3.up, rotateValue * Time.deltaTime * 10);
        
    }
}
